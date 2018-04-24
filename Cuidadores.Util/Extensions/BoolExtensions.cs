namespace Cuidadores.Util.Extensions
{
    public static class BoolExtensions
    {
        public static string ToStringBool(this bool value)
        {
            return value ? "Sim" : "Não";
        }

        public static string ToStringBool(this bool? value)
        {
            return value == null ? "Não" : ToStringBool(value.Value);
        }
    }
}
