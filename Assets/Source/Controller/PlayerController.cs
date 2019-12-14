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
        float msens = 0.1f;//move senitivity

        public PlayerController() {

        }

        public void update() {
            look();
            move();
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
            Client.model.player.vel *= 0.8f;
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
            Client.model.player.pos += Client.model.player.vel;
            IntVec3 chunkpos = (Client.model.player.pos / 16).Floor();
            if (chunkpos == Client.model.map.chunkpos)
                return;
            Client.model.map.setChunkPos(chunkpos);
            Client.view.world.move(chunkpos - Client.model.map.chunkpos);
        }

        void point() {
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            bool result = Physics.Raycast(ray, out hit, 10000f);
        }
    }
}
