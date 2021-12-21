﻿using System;
using GTA;
using GTA.Native;
using GTA.Math;
using LittleJacobMod.Saving;
using LittleJacobMod.Interface;
using LittleJacobMod.Utils;

class WeaponPreview : Script
{
    Prop _weaponHandle;
    bool _doComponentReload;
    bool _doObjectSpawn;
    bool _doComponentChange;
    bool _compFromStorage;
    uint _hash = 453432689;
    uint _new = (uint)WeaponComponentHash.Invalid;
    ComponentIndex _skipIndex;

    public WeaponPreview()
    {
        Menu.ComponentSelected += Menu_ComponentSelected;
        Menu.SpawnWeaponObject += Menu_SpawnWeaponObject;
        Menu.CamoColorChanged += Menu_CamoColorChanged;
        Menu.TintChanged += Menu_TintChanged;

        LittleJacob.TrunkStateChanged += LittleJacob_TrunkStateChanged;

        Tick += WeaponPreview_Tick;
    }

    private void WeaponPreview_Tick(object sender, EventArgs e)
    {
        if (_doComponentReload)
        {
            SpawnWeaponObject(_hash);
            _doComponentReload = false;
            _compFromStorage = false;
        }

        if (_doObjectSpawn)
        {
            SpawnWeaponObject(_hash);
            _doObjectSpawn = false;
            _compFromStorage = false;
        }

        if (_doComponentChange)
        {
            GiveWeaponComponentToObject(_new);

            if (_new != (uint)WeaponComponentHash.Invalid)
            {
                var slide = TintsAndCamos.ReturnSlide(_new);

                if (slide != (uint)WeaponComponentHash.Invalid)
                {
                    GiveWeaponComponentToObject(slide);
                }
            }

            _doComponentChange = false;
        }
    }

    private void LittleJacob_TrunkStateChanged(object sender, bool opened)
    {
        if (!opened && _weaponHandle != null && _weaponHandle.Handle != 0)
        {
            DeleteWeaponObject();
        }
    }

    private void Menu_TintChanged(object sender, int e)
    {
        Function.Call(Hash.SET_WEAPON_OBJECT_TINT_INDEX, _weaponHandle.Handle, e);
    }

    private void Menu_CamoColorChanged(object sender, CamoColorEventArgs e)
    {
        Function.Call(Hash._SET_WEAPON_OBJECT_LIVERY_COLOR, _weaponHandle.Handle, e.Camo, e.ColorIndex);

        var slide = TintsAndCamos.ReturnSlide(e.Camo);

        if (slide != (uint)WeaponComponentHash.Invalid)
        {
            Function.Call(Hash._SET_WEAPON_OBJECT_LIVERY_COLOR, _weaponHandle.Handle, slide, e.ColorIndex);
        }
    }

    private void LoadAttachments(uint hash)
    {
        if (LoadoutSaving.IsWeaponInStore(hash))
        {
            var storedWeapon = LoadoutSaving.GetStoreReference(hash);
            Function.Call(Hash.SET_WEAPON_OBJECT_TINT_INDEX, _weaponHandle.Handle, storedWeapon.GetTintIndex());

            if (SkipComponent(storedWeapon.Camo, ComponentIndex.Livery))
            {
                GiveWeaponComponentToObject(storedWeapon.Camo);

                var slide = TintsAndCamos.ReturnSlide(storedWeapon.Camo);

                if (slide != (uint)WeaponComponentHash.Invalid)
                {
                    GiveWeaponComponentToObject(slide);
                    Function.Call(Hash._SET_WEAPON_OBJECT_LIVERY_COLOR, _weaponHandle.Handle, slide, storedWeapon.GetCamoColor());
                }

                Function.Call(Hash._SET_WEAPON_OBJECT_LIVERY_COLOR, _weaponHandle.Handle, storedWeapon.Camo, storedWeapon.GetCamoColor());
            }

            if (SkipComponent(storedWeapon.Barrel, ComponentIndex.Barrel))
            {
                GiveWeaponComponentToObject(storedWeapon.Barrel);
            }

            if (SkipComponent(storedWeapon.Clip, ComponentIndex.Clip))
            {
                GiveWeaponComponentToObject(storedWeapon.Clip);
            }

            if (SkipComponent(storedWeapon.Flashlight, ComponentIndex.Flashlight))
            {
                GiveWeaponComponentToObject(storedWeapon.Flashlight);
            }

            if (SkipComponent(storedWeapon.Grip, ComponentIndex.Grip))
            {
                GiveWeaponComponentToObject(storedWeapon.Grip);
            }

            if (SkipComponent(storedWeapon.Scope, ComponentIndex.Scope))
            {
                GiveWeaponComponentToObject(storedWeapon.Scope);
            }

            if (SkipComponent(storedWeapon.Muzzle, ComponentIndex.Muzzle))
            {
                GiveWeaponComponentToObject(storedWeapon.Muzzle);
            }
        }
    }

    private bool SkipComponent(uint component, ComponentIndex index)
    {
        return component != (uint)WeaponComponentHash.Invalid && (_compFromStorage || (!_compFromStorage && index != _skipIndex));
    }

    private void Menu_SpawnWeaponObject(object sender, uint hash)
    {
        _doObjectSpawn = true;
        _hash = hash;
        _compFromStorage = true;
    }

    private void SpawnWeaponObject(uint hash)
    {
        DeleteWeaponObject();

        Function.Call(Hash.REQUEST_WEAPON_ASSET, hash, 31, 0);

        while (!Function.Call<bool>(Hash.HAS_WEAPON_ASSET_LOADED, hash))
        {
            Wait(1);
        }

        _weaponHandle = Function.Call<Prop>(Hash.CREATE_WEAPON_OBJECT, hash, 1, Main.LittleJacob.Vehicle.RearPosition.X + (Main.cam.Direction.X / 1.4f), Main.LittleJacob.Vehicle.RearPosition.Y + (Main.cam.Direction.Y / 1.4f), Main.LittleJacob.Vehicle.RearPosition.Z + 0.15f, true, 1, 0);
        _weaponHandle.PositionNoOffset = new Vector3(Main.LittleJacob.Vehicle.RearPosition.X + (Main.cam.Direction.X / 1.2f), Main.LittleJacob.Vehicle.RearPosition.Y + (Main.cam.Direction.Y / 1.2f), Main.LittleJacob.Vehicle.RearPosition.Z + 0.4f);
        _weaponHandle.HasGravity = false;
        _weaponHandle.IsCollisionEnabled = false;
        _weaponHandle.Heading = Main.cam.ForwardVector.ToHeading();
        _weaponHandle.Rotation = new Vector3(_weaponHandle.Rotation.X, _weaponHandle.Rotation.Y, _weaponHandle.Rotation.Z - 10);
        Function.Call(Hash.REMOVE_WEAPON_ASSET, hash);

        LoadAttachments(hash);
    }

    private void Menu_ComponentSelected(object sender, ComponentPreviewEventArgs component)
    {
        _doComponentChange = true;
        _new = component.PreviewComponent;
        _skipIndex = component.ComponentIndex;
        _hash = component.WeaponHash;
    }

    private void GiveWeaponComponentToObject(uint component)
    {
        if (component == (uint)WeaponComponentHash.Invalid)
        {
            SpawnWeaponObject(_hash);
            return;
        }

        int componentModel = Function.Call<int>(Hash.GET_WEAPON_COMPONENT_TYPE_MODEL, component);
        if (componentModel != 0)
        {
            Function.Call(Hash.REQUEST_MODEL, componentModel);

            while (!Function.Call<bool>(Hash.HAS_MODEL_LOADED, componentModel))
            {
                Wait(1);
            }

            Function.Call(Hash.GIVE_WEAPON_COMPONENT_TO_WEAPON_OBJECT, _weaponHandle.Handle, component);
            Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, componentModel);
        }
    }

    public void DeleteWeaponObject()
    {
        if (_weaponHandle != null && _weaponHandle.Handle != 0)
        {
            _weaponHandle.Delete();
        }
    }
}
