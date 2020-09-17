using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;

namespace BIEMM.Utils
{
    public static class ModTypeChecker
    {
        public static ModTypes GetModType(FileInfo modFile)
        {
            // Patch checker from PartialityLauncher
            try
            {
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
            catch (Exception exception)
            {
                Logger.ErrorLog(exception.Message, exception.StackTrace, exception.Source);
                throw;
            }

        }
    }
}