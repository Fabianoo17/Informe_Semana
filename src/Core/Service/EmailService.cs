using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class EmailService
    {
        public static void EnviaMensagem(string assunto, string conteudo, List<string> listaMatriculaTo, List<string> listaMatriculaCC = null, List<string> listaMatriculaBcc = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("ceratbr@caixa.gov.br", "Não responder - CETEC - CN TECNOLOGIA DA INFORMAÇÃO", UTF32Encoding.UTF32);

                if (listaMatriculaTo.Count() == 0)
                    throw new Exception("listaMatriculaTo é obrigatório.");

                foreach (var to in listaMatriculaTo)
                {
                    mail.To.Add(new MailAddress(to + "@mail.caixa"));
                }

                if (listaMatriculaCC != null)
                {
                    foreach (var cc in listaMatriculaCC)
                    {
                        mail.CC.Add(new MailAddress(cc + "@mail.caixa"));
                    }
                }

                if (listaMatriculaBcc != null)
                {
                    foreach (var bcc in listaMatriculaCC)
                    {
                        mail.Bcc.Add(new MailAddress(bcc + "@mail.caixa"));
                    }
                }

                mail.IsBodyHtml = true;
                mail.Subject = assunto;
                mail.Body = conteudo;

                Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void Send(MailMessage mail)
        {
            if (!ServerUtilities.IsLocalhost())
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "sistemas.correiolivre.caixa";
                smtp.Send(mail);
            }
        }
    }
}
