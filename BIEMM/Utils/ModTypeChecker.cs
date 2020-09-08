using System.IO;

namespace BIEMM.Utils
{
    public static class ModTypeChecker
    {
        public static ModTypes GetModType(FileInfo modFile)
        {
            if (modFile.Directory.FullName == Utils.PathList.PatchPath || modFile.Directory.FullName == Utils.PathList.BepPatchPath)
            {
                return ModTypes.Patch;
            }

            return ModTypes.Mod;

            /* https://discordapp.com/channels/291184728944410624/305139167300550666/752915891980730450
             
            Patch checker from PartialityLauncher
            
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
            }*/


        }
    }
}