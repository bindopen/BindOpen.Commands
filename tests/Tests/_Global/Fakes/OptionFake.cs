using BindOpen.Data.Meta;
using BindOpen.Scoping;

namespace BindOpen.Plus.Commands.Tests
{
    /// <summary>
    /// This class represents a fake class.
    /// </summary>
    [BdoEntity()]
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