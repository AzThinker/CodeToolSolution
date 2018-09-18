using MetaWorkLib.Config;
using System.Windows.Forms;

namespace WinCodeView
{
    public class AtkForm : Form
    {
        protected AzNormalSet azNormalSet;

        public AtkForm()
        {
            var azNormal = AzNormalSet.GetAzNormalSet();
            if (azNormal != null)
            {
                azNormalSet = azNormal;
            }

        }
    }
}
