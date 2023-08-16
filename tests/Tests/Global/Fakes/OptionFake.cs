using BindOpen.System.Data.Meta;

namespace BindOpen.Tests.Commands
{
    /// <summary>
    /// This class represents a fake class.
    /// </summary>
    public class OptionFake
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        [BdoProperty("help")]
        public bool Help { get; set; }

        [BdoProperty("version")]
        public string Version { get; set; }

        [BdoProperty("input")]
        public int? Input { get; set; }

        #endregion

        public OptionFake()
        {
        }
    }
}