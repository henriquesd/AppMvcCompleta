using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevIO.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificador _notificador;

        public SummaryViewComponent(INotificador notificador)
        {
            _notificador = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Task.FromResult - como este método ObterNotificacoes não é assíncrono, e aqui está dentro
            // de um contexto assíncrono, deve receber ele desta forma para possibiliar a compatibilidade;
            var notificacoes = await Task.FromResult(_notificador.ObterNotificacoes()); // obtém todas as notificações;
            
            // irá adicionar cada notificao na ModelState, como se fosse um erro de Model, ou seja, ele irá tratar no formulário
            // como se fosse um erro de preenchimento de campo, mas sem um campo específico;
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Mensagem));

            return View();
        }
    }
}
