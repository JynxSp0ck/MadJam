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
            Client.model.player.vel *= 0.95f;
            relacc.y = -0.01f;
            Block block = Client.model.map.getBlock((Client.model.player.pos - new Vec3(0, 0.01f, 0)).Floor());
            if (block.type != BlockType.get("air")) {
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
                Client.model.player.vel.x *= 0.8f;
                Client.model.player.vel.z *= 0.8f;
                if (Input.GetKey(KeyCode.Space)) {
                    relacc.y += 0.5f;
                }
            }
            if (relacc.mag() == 0 && Client.model.player.vel.mag() < 0.01f)
                Client.model.player.vel *= 0;
            Client.model.player.vel.x += relacc.x * (float)Math.Cos(Client.view.camera.ha * conv) + relacc.z * (float)Math.Sin(Client.view.camera.ha * conv);
            Client.model.player.vel.z += relacc.z * (float)Math.Cos(Client.view.camera.ha * conv) - relacc.x * (float)Math.Sin(Client.view.camera.ha * conv);
            Client.model.player.vel.y += relacc.y;

            
            if(block.type != BlockType.get("air") && Client.model.player.vel.y < 0) {
                Client.model.player.vel.y = 0;
            }
            Block blocknx = Client.model.map.getBlock((Client.model.player.pos + new Vec3(-0.05f, 1, 0)).Floor());
            Block blockpx = Client.model.map.getBlock((Client.model.player.pos + new Vec3(0.05f, 1, 0)).Floor());
            Block blocknz = Client.model.map.getBlock((Client.model.player.pos + new Vec3(0, 1, -0.05f)).Floor());
            Block blockpz = Client.model.map.getBlock((Client.model.player.pos + new Vec3(0, 1, 0.05f)).Floor());
            Block blocknx2 = Client.model.map.getBlock((Client.model.player.pos + new Vec3(-0.05f, 2, 0)).Floor());
            Block blockpx2 = Client.model.map.getBlock((Client.model.player.pos + new Vec3(0.05f, 2, 0)).Floor());
            Block blocknz2 = Client.model.map.getBlock((Client.model.player.pos + new Vec3(0, 2, -0.05f)).Floor());
            Block blockpz2 = Client.model.map.getBlock((Client.model.player.pos + new Vec3(0, 2, 0.05f)).Floor());

            if (Client.model.player.vel.x < 0 && (blocknx.type != BlockType.get("air") || blocknx2.type != BlockType.get("air"))) {
                Client.model.player.vel.x = 0;
            }

            if (Client.model.player.vel.x > 0 && (blockpx.type != BlockType.get("air") || blockpx2.type != BlockType.get("air"))) {
                Client.model.player.vel.x = 0;
            }
            if (Client.model.player.vel.z < 0 && (blocknz.type != BlockType.get("air") || blocknz2.type != BlockType.get("air"))) {
                Client.model.player.vel.z = 0;
            }
            if (Client.model.player.vel.z > 0 && (blockpz.type != BlockType.get("air") || blockpz2.type != BlockType.get("air"))) {
                Client.model.player.vel.z = 0;
            }

            Client.model.player.pos += Client.model.player.vel;
            IntVec3 chunkpos = (Client.model.player.pos / 16).Floor();
            if (chunkpos == Client.model.map.chunkpos)
                return;
            IntVec3 delta = chunkpos - Client.model.map.chunkpos;
            Client.model.map.setChunkPos(chunkpos);
            Client.view.world.move(delta);
        }

        IntVec3 point() {
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            bool result = Physics.Raycast(ray, out hit, 10000f);
            if (result == false)
                return null;
            IntVec3 point = (Conv.ert(hit.point) + Conv.ert(ray.direction) / 100).Floor();
            return point;
        }

        void mineBlock() {
            if (Input.GetButton("Fire1")) {
                IntVec3 Point = point();
                Block block = Client.model.map.getBlock(Point);
                block.type = BlockType.get("air");
                IntVec3 index = Client.model.map.getChunkIndex(Point);
                if (index != null) {
                    Client.model.map.chunks[index.x, index.y, index.z].depricate();
                    Client.view.world.chunks[index.x, index.y, index.z].depricate();
                }
            }  
        }
    }
}
