using System;
using System.Collections.Generic;
using System.IO;
using BIEMM.Enums;
using Mono.Cecil;

namespace BIEMM.Utils.ModManaging
{
    public static class ModTypeChecker
    {
        public static ModTypes GetModType(FileInfo modFile)
        {
            // Patch checker from PartialityLauncher

            ModuleDefinition modDef = ModuleDefinition.ReadModule(modFile.FullName);

            Type patchType = typeof(MonoMod.MonoModPatch);

            //Foreach type in the mod dll
            foreach (TypeDefinition checkType in modDef.GetTypes())
            {
                //If the type has a custom attribute
                if (checkType.HasCustomAttributes)
                {
                    HashSet<CustomAttribute> attributes = new HashSet<CustomAttribute>(checkType.CustomAttributes);
                    //Foreach custom attribute
                    foreach (CustomAttribute ct in attributes)
                    {

                        //If the attribute is [MonoModPatch]
                        if (ct.AttributeType.Name == patchType.Name)
                        {
                            modDef.Dispose();
                            return ModTypes.Patch;
                        }

                    }
                }
            }

            modDef.Dispose();
            return ModTypes.Mod;

        }
    }
}