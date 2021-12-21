﻿using System;
using System.Collections.Generic;
using GTA;

namespace LittleJacobMod.Utils.Weapons
{
    internal class Minigun : Weapon
    {
        public override bool SaveFileWeapon => true;

        public override uint WeaponHash => (uint)GTA.WeaponHash.Minigun;

        public override int Price => 80000;

        public override string Name => "Minigun";

        public override bool HasMuzzleOrSupp => false;

        public override bool HasClip => false;

        public override bool HasBarrel => false;

        public override bool HasGrip => false;

        public override bool HasScope => false;

        public override bool HasCamo => false;

        public override bool HasFlaslight => false;

        public override Dictionary<string, uint> MuzzlesAndSupps => throw new NotImplementedException();

        public override Dictionary<string, uint> Clips => throw new NotImplementedException();

        public override Dictionary<string, uint> Barrels => throw new NotImplementedException();

        public override Dictionary<string, uint> Grips => throw new NotImplementedException();

        public override Dictionary<string, uint> Scopes => throw new NotImplementedException();

        public override Dictionary<string, uint> Camos => throw new NotImplementedException();

        public override Dictionary<string, uint> FlashLight => throw new NotImplementedException();
    }
}
