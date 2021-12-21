﻿using System;
using System.Collections.Generic;
using GTA;

namespace LittleJacobMod.Utils.Weapons
{
    internal class MarksmanRifleMk2 : Weapon
    {
        public override bool SaveFileWeapon => true;

        public override uint WeaponHash => (uint)GTA.WeaponHash.MarksmanRifleMk2;

        public override int Price => 112000;

        public override string Name => "Marksman Rifle Mk2";

        public override bool HasMuzzleOrSupp => true;

        public override bool HasClip => true;

        public override bool HasBarrel => true;

        public override bool HasGrip => true;

        public override bool HasScope => true;

        public override bool HasCamo => true;

        public override bool HasFlaslight => true;

        public override Dictionary<string, uint> MuzzlesAndSupps => new Dictionary<string, uint>()
        {
            { "None - $199", (uint)WeaponComponentHash.Invalid },
            { "Suppressor - $40000", (uint)WeaponComponentHash.AtArSupp },
            { "Flat Muzzle - $29000", (uint)WeaponComponentHash.AtMuzzle01 },
            { "Tactical Muzzle - $31000", (uint)WeaponComponentHash.AtMuzzle02 },
            { "Fat End Muzzle - $32000", (uint)WeaponComponentHash.AtMuzzle03 },
            { "Precision Muzzle - $34000", (uint)WeaponComponentHash.AtMuzzle04 },
            { "Heavy Duty Muzzle - $35000", (uint)WeaponComponentHash.AtMuzzle05 },
            { "Slanted Muzzle - $37000", (uint)WeaponComponentHash.AtMuzzle06 },
            { "Split End Muzzle - $38000", (uint)WeaponComponentHash.AtMuzzle07 }
        };

        public override Dictionary<string, uint> Clips => new Dictionary<string, uint>()
        {
            { "Normal - $199", (uint)WeaponComponentHash.MarksmanRifleMk2Clip01 },
            { "Extended - $25000", (uint)WeaponComponentHash.MarksmanRifleMk2Clip02 },
            { "Tracer - $44000", (uint)WeaponComponentHash.MarksmanRifleMk2ClipTracer },
            { "Incendiary - $51000", (uint)WeaponComponentHash.MarksmanRifleMk2ClipIncendiary },
            { "Armor piercing - $66000", (uint)WeaponComponentHash.MarksmanRifleMk2ClipArmorPiercing },
            { "FMJ - $76000", (uint)WeaponComponentHash.MarksmanRifleMk2ClipFMJ },
        };

        public override Dictionary<string, uint> Barrels => new Dictionary<string, uint>()
        {
            { "Standard - $199", (uint)WeaponComponentHash.AtMrFlBarrel01 },
            { "Heavy - $49000", (uint)WeaponComponentHash.AtMrFlBarrel02 },
        };

        public override Dictionary<string, uint> Grips => new Dictionary<string, uint>()
        {
            { "None - $199", (uint)WeaponComponentHash.Invalid },
            { "Grip - $14000", (uint)WeaponComponentHash.AtArAfGrip02 },
        };

        public override Dictionary<string, uint> Scopes => new Dictionary<string, uint>()
        {
            { "Default - $199", (uint)WeaponComponentHash.AtScopeLargeFixedZoomMk2 },
            { "Large - $15000", (uint)WeaponComponentHash.AtScopeMediumMk2 },
            { "Holographic - $8000", (uint)WeaponComponentHash.AtSights }
        };

        public override Dictionary<string, uint> Camos => new Dictionary<string, uint>()
        {
            { "None", (uint)WeaponComponentHash.Invalid },
            { "Digital", (uint)WeaponComponentHash.MarksmanRifleMk2Camo },
            { "Brushstroke", (uint)WeaponComponentHash.MarksmanRifleMk2Camo02 },
            { "Woodland", (uint)WeaponComponentHash.MarksmanRifleMk2Camo03 },
            { "Skull", (uint)WeaponComponentHash.MarksmanRifleMk2Camo04 },
            { "Sessanta Nove", (uint)WeaponComponentHash.MarksmanRifleMk2Camo05 },
            { "Perseus", (uint)WeaponComponentHash.MarksmanRifleMk2Camo06 },
            { "Leopard", (uint)WeaponComponentHash.MarksmanRifleMk2Camo07 },
            { "Zebra", (uint)WeaponComponentHash.MarksmanRifleMk2Camo08 },
            { "Geometric", (uint)WeaponComponentHash.MarksmanRifleMk2Camo09 },
            { "Boom!", (uint)WeaponComponentHash.MarksmanRifleMk2Camo10 },
            { "Patriotic", (uint)WeaponComponentHash.MarksmanRifleMk2CamoIndependence01 },

        };

        public override Dictionary<string, uint> FlashLight => new Dictionary<string, uint>()
        {
            { "None - $199", (uint)WeaponComponentHash.Invalid },
            { "Flashlight - $10000", (uint)WeaponComponentHash.AtArFlsh }
        };
    }
}
