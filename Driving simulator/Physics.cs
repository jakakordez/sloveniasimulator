﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletSharp;
using OpenTK;

namespace Driving_simulator
{
    class Physics
    {
        public DiscreteDynamicsWorld World { get; set; }
        CollisionDispatcher dispatcher;
        DbvtBroadphase broadphase;
        CollisionConfiguration collisionConf;
        public Vehicle[] Vehicles = new Vehicle[0];
        public Vehicle Player;
        public Physics(ref MeshCollector meshCollection)
        {
            // collision configuration contains default setup for memory, collision setup
            collisionConf = new DefaultCollisionConfiguration();
            dispatcher = new CollisionDispatcher(collisionConf);

            broadphase = new DbvtBroadphase();
            World = new DiscreteDynamicsWorld(dispatcher, broadphase, null, collisionConf);
            World.Gravity = new Vector3(0, -10, 0);

            LocalCreateRigidBody(0, Matrix4.CreateTranslation(-50*Vector3.UnitY), new BoxShape(500, 50, 500));
            addCar("BMW/M3-E92", Matrix4.CreateTranslation(new Vector3(0, 1, 0)), true, ref meshCollection);//
            for (int i = 0; i < 5; i++)
            {
                addCar("BMW/M3-E92", Matrix4.CreateTranslation(new Vector3(10, 1, i*10)), false, ref meshCollection);
            }
            //a = System.IO.File.OpenRead("data/maps/map1/h.raw");
            //HeightfieldTerrainShape t = new HeightfieldTerrainShape(128, 128, a, 1, -100, 100, 1, PhyScalarType.PhyFloat, false);
            //LocalCreateRigidBody(0, Matrix4.CreateTranslation(new Vector3(-64, -20, -64)), t);
            //LocalCreateRigidBody(0, Matrix4.CreateTranslation(new Vector3(15, 0, 0)), new BoxShape(5, 5, 5));
            //map = new Maps.Map();
            //RigidBody ground = LocalCreateRigidBody(0, map.tr, map.groundShape);
            //ground.UserObject = "Ground";
        }

        public void Update(float elaspedTime, OpenTK.Input.KeyboardDevice k)
        {
            Player.raycastVehicle.UpdateAction(World, elaspedTime);
            (Player as Vehicles.Car).Update(elaspedTime, k);
            for (int i = 0; i < Vehicles.Length; i++)
            {
                Vehicles[i].Update(elaspedTime, null);
            }
            World.StepSimulation(elaspedTime);
        }

        public void addCar(string path, Matrix4 startTransform, bool player, ref MeshCollector meshCollection)
        {
            Vehicles.Car a = new Vehicles.Car(path, 4, ref meshCollection);
            a.body = LocalCreateRigidBody(a.Mass, startTransform, a.collisionShape);
            a.Init(new DefaultVehicleRaycaster(World));
            World.AddAction(a.raycastVehicle);
            if (player && Player == null) Player = a;
            else Misc.Push<Vehicle>(a, ref Vehicles);
        }

        public RigidBody LocalCreateRigidBody(float mass, Matrix4 startTransform, CollisionShape shape)
        {
            bool isDynamic = (mass != 0.0f);

            Vector3 localInertia = Vector3.Zero;
            if (isDynamic)
                shape.CalculateLocalInertia(mass, out localInertia);

            DefaultMotionState myMotionState = new DefaultMotionState(startTransform);

            RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
            RigidBody body = new RigidBody(rbInfo);

            World.AddRigidBody(body);
            //World.AddCollisionObject(body);
            return body;
        }
        public void ExitPhysics()
        {
            //remove/dispose constraints
            int i;
            for (i = World.NumConstraints - 1; i >= 0; i--)
            {
                TypedConstraint constraint = World.GetConstraint(i);
                World.RemoveConstraint(constraint);
                constraint.Dispose();
            }

            //remove the rigidbodies from the dynamics world and delete them
            for (i = World.NumCollisionObjects - 1; i >= 0; i--)
            {
                CollisionObject obj = World.CollisionObjectArray[i];
                RigidBody body = obj as RigidBody;
                if (body != null && body.MotionState != null)
                {
                    body.MotionState.Dispose();
                }
                World.RemoveCollisionObject(obj);
                obj.Dispose();
            }

            World.Dispose();
            broadphase.Dispose();
            if (dispatcher != null)
            {
                dispatcher.Dispose();
            }
            collisionConf.Dispose();
        }
    }
}
