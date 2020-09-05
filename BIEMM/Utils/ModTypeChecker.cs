using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace BIEMM.Utils
{
    public static class ModTypeChecker
    {
        //Patch checker from PartialityLauncher
        public static ModTypes GetModType(string modFile)
        {
            try
            {
                ModuleDefinition modDef = ModuleDefinition.ReadModule(modFile);

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
            catch (Exception)
            {
                return ModTypes.None;
            }


        }
    }
}