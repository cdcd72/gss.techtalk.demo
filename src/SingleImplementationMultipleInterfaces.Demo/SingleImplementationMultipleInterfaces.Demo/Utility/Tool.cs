namespace SingleImplementationMultipleInterfaces.Demo.Utility
{
    public class Tool : ISecurityTool, IHashTool
    {
        #region Security Related

        public bool IsSecurity() => true;

        #endregion

        #region Hash Related

        public string Hash() => "Hashed Value...";

        #endregion
    }
}
