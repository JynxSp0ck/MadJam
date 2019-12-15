using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Model;
using Game.View;
using Game.Utility;

namespace Game.Controller {
    class PlayerController {
        Vec2 mouse = new Vec2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        public float conv = 3.14159265358979f / 180f;
        float lsens = 5f;//lookove senitivity
        float msens = 0.01f;//move senitivity

        public PlayerController() {

        }

        public void update() {
            look();
            move();
            mineBlock();
            placeBlock();
        }

        void look() {
            mouse = new Vec2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * lsens;
            Client.view.camera.ha += mouse.x;
            Client.view.camera.va -= mouse.y;
            if (Client.view.camera.ha >= 360)
                Client.view.camera.ha -= 360;
            if (Client.view.camera.ha < 0)
                Client.view.camera.ha += 360;
            if (Client.view.camera.va > 90)
                Client.view.camera.va = 90;
            if (Client.view.camera.va < -90)
                Client.view.camera.va = -90;
        }

        void move() {
            Vec3 relacc = new Vec3(0, 0, 0);
            Client.model.player.vel.y *= 0.95f;
            Client.model.player.vel.x *= 0.9f;
            Client.model.player.vel.z *= 0.9f;
            relacc.y = -0.01f;
            Block block = Client.model.map.getBlock((Client.model.player.pos - new Vec3(0, 0.01f, 0)).Floor());
            float mspeed = msens;
            if (block.type != BlockType.get("air")) {
                if (Input.GetKey(KeyCode.Space)) {
                    relacc.y += 0.25f;
                }
            }
            else {
                mspeed /= 2;
            }
            if (Input.GetKey(KeyCode.W)) {
                relacc.z += msens;
            }
            if (Input.GetKey(KeyCode.S)) {
                relacc.z -= msens;
            }
            if (Input.GetKey(KeyCode.D)) {
                relacc.x += msens;
            }
            if (Input.GetKey(KeyCode.A)) {
                relacc.x -= msens;
            }
            if (relacc.mag() == 0 && Client.model.player.vel.mag() < 0.01f)
                Client.model.player.vel *= 0;
            Client.model.player.vel.x += relacc.x * (float)Math.Cos(Client.view.camera.ha * conv) + relacc.z * (float)Math.Sin(Client.view.camera.ha * conv);
            Client.model.player.vel.z += relacc.z * (float)Math.Cos(Client.view.camera.ha * conv) - relacc.x * (float)Math.Sin(Client.view.camera.ha * conv);
            Client.model.player.vel.y += relacc.y;

            Vec3 nextpos = Client.model.player.pos + Client.model.player.vel;

            if (collide(Client.model.player.pos + new Vec3(0, Client.model.player.vel.y, 0)))
                Client.model.player.vel.y = 0;
            Client.model.player.pos.y += Client.model.player.vel.y;
            if (collide(Client.model.player.pos + new Vec3(Client.model.player.vel.x, 0, 0)))
                Client.model.player.vel.x = 0;
            Client.model.player.pos.x += Client.model.player.vel.x;
            if (collide(Client.model.player.pos + new Vec3(0, 0, Client.model.player.vel.z)))
                Client.model.player.vel.z = 0;
            Client.model.player.pos.z += Client.model.player.vel.z;

            IntVec3 chunkpos = (Client.model.player.pos / 16).Floor();
            if (chunkpos == Client.view.world.chunkpos)
                return;
            IntVec3 delta = chunkpos - Client.view.world.chunkpos;
            Client.view.world.move(delta);
        }

        bool collide(Vec3 pos) {
            bool result = false;

            result = result || pointCollide(pos + new Vec3(0.25f, 0, 0.25f));
            result = result || pointCollide(pos + new Vec3(0.25f, 0, -0.25f));
            result = result || pointCollide(pos + new Vec3(-0.25f, 0, 0.25f));
            result = result || pointCollide(pos + new Vec3(-0.25f, 0, -0.25f));

            result = result || pointCollide(pos + new Vec3(0.25f, 0.5f, 0.25f));
            result = result || pointCollide(pos + new Vec3(0.25f, 0.5f, -0.25f));
            result = result || pointCollide(pos + new Vec3(-0.25f, 0.5f, 0.25f));
            result = result || pointCollide(pos + new Vec3(-0.25f, 0.5f, -0.25f));

            result = result || pointCollide(pos + new Vec3(0.25f, 1, 0.25f));
            result = result || pointCollide(pos + new Vec3(0.25f, 1, -0.25f));
            result = result || pointCollide(pos + new Vec3(-0.25f, 1, 0.25f));
            result = result || pointCollide(pos + new Vec3(-0.25f, 1, -0.25f));

            result = result || pointCollide(pos + new Vec3(0.25f, 1.5f, 0.25f));
            result = result || pointCollide(pos + new Vec3(0.25f, 1.5f, -0.25f));
            result = result || pointCollide(pos + new Vec3(-0.25f, 1.5f, 0.25f));
            result = result || pointCollide(pos + new Vec3(-0.25f, 1.5f, -0.25f));

            result = result || pointCollide(pos + new Vec3(0.25f, 1.75f, 0.25f));
            result = result || pointCollide(pos + new Vec3(0.25f, 1.75f, -0.25f));
            result = result || pointCollide(pos + new Vec3(-0.25f, 1.75f, 0.25f));
            result = result || pointCollide(pos + new Vec3(-0.25f, 1.75f, -0.25f));

            return result;
        }

        bool pointCollide(Vec3 pos) {
            return Client.model.map.getBlock(pos.Floor()).type.name != "air";
        }

        Vec3 pointmine() {
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            bool result = Physics.Raycast(ray, out hit, 10000f);
            if (result == false)
                return null;
            return Conv.ert(hit.point) + Conv.ert(ray.direction) / 100;
        }

        Vec3 pointplace() {
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            bool result = Physics.Raycast(ray, out hit, 10000f);
            if (result == false)
                return null;
            return Conv.ert(hit.point) - Conv.ert(ray.direction) / 100;
        }

        void mineBlock() {
            if (Input.GetMouseButton(0)) {
                Client.model.player.character.dig += Client.model.player.character.digspeed;
                if (Client.model.player.character.dig > 1) {
                    Client.model.player.character.dig = 0;
                    Vec3 point = pointmine();
                    float dist = (point - (Client.model.player.pos + new Vec3(0, 1, 0))).mag();
                    if (dist <= Client.model.player.character.range) {
                        IntVec3 bindex = point.Floor();
                        Block block = Client.model.map.getBlock(bindex);
                        if (block.type.mineable)
                            Client.model.player.character.inventory.add(new Stack(block.type, 1));
                        block.type = BlockType.get("air");
                        Chunk chunk = Client.model.map.getChunk(bindex);
                        if (chunk != null) {
                            chunk.depricate();
                            Client.view.world.blockUpdate(bindex);
                        }
                    }
                }
            }
            else {
                Client.model.player.character.dig = 0;
            }
        }

        void placeBlock() {
            if (Input.GetMouseButtonDown(1)) {
                Vec3 point = pointplace();
                float dist = (point - (Client.model.player.pos + new Vec3(0, 1, 0))).mag();
                if (dist <= Client.model.player.character.range) {
                    IntVec3 bindex = point.Floor();
                    Block block = Client.model.map.getBlock(bindex);
                    Stack stack = Client.model.player.character.inventory.slots[Client.view.ui.inventoryview.getSelected()];
                    if (stack.type != null) {
                        if (stack.count > 0 && stack.type.mineable) {
                            stack.count--;
                            block.type = stack.type;
                        }
                    }
                    Chunk chunk = Client.model.map.getChunk(bindex);
                    if (chunk != null) {
                        chunk.depricate();
                        Client.view.world.blockUpdate(bindex);
                    }
                }
            }
        }
    }
}
