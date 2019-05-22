using Phenix.Challenge.Services;

namespace Phenix.Challenge.Infrastructure
{
    public class Registry : StructureMap.Registry
    {
        public Registry()
        {
            For<ILecteurFichier>().Use<LecteurFichier>();
        }
    }
}
