using Source.DTOs.Request;
using Source.Handlers;
using UnityEngine;

namespace UI
{
    public class CodeOpener : LinkOpener
    {
        [SerializeField] private CodeView codeView;

        public override void OpenLink()
        {
            ClickCodeDto code = new()
            {
                url = link
            };
            
            MetricsHandler.CodeClicked(code);
            
            codeView.CopyCode();
            
            base.OpenLink();
        }
    }
}