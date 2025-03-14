using System.Net.NetworkInformation;
using System.Reflection;

namespace GestionDeTareas.API.Servicios
{
    public class PresentationAssemblyReference
    {
        internal static readonly Assembly assembly = typeof(PresentationAssemblyReference).Assembly;
    }
}
