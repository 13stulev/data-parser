using Org;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org
{
    internal interface IWriteImplementation
    {
        public void write(StreamWriter writer, Organisation org, int k);
        public void uniteFiles(int n, string filename = "total");
    }

    internal class TextWriter : IWriteImplementation
    {
        public void uniteFiles(int n, string filename)
        {
            StreamWriter writer = new StreamWriter($"{filename}.txt", false);
            // ] используется как разделитель
            writer.WriteLine("id]org_fullname]org_shortName]ogrn]inn]kpp]grbs_code]chief]phone]url]region]okato]oktmo]ogrn_date]okveds]");
            for (int i = 1; i <= n; i++)
            {
                StreamReader reader = new StreamReader($"text{i}.txt");
                writer.Write(reader.ReadToEnd());
                reader.Close();
            }
            writer.Close();
        }

        public void write(StreamWriter writer, Organisation org, int k)
        {
            foreach (Content item in org.pagedContent.content)
            {
                Initiator init = item.body.position.initiator;
                MainContent con = item.body.position.main;
                Additional add = item.body.position.additional;
                Other etc = item.body.position.other;
                string okveds = "";
                if (add.activity != null)
                {
                    foreach (Org.Activity? names in add.activity)
                    {
                        if (names.okved != null)
                        {
                            okveds += (names.okved.name ?? "") + " ";
                        }
                    }
                    okveds = okveds.Substring(0, 4000);
                }
                string? chief = "";
                if (etc.chief != null)
                {
                    chief = (etc.chief[0].lastName ?? "") + " " + (etc.chief[0].firstName ?? "") + " " + (etc.chief[0].midddleName ?? "");
                    chief.Trim();
                }
                string okatoCode = "";
                if (add.ppo is not null)
                {
                    if (add.ppo.okato is not null) okatoCode = add.ppo.okato.code ?? "";
                    writer.WriteLine(k++ + "]" + (init.fullName ?? "") + "]" + (con.shortName ?? "") + "]" + (con.ogrn ?? "") + "]" + (init.inn ?? "") + "]" + (init.kpp ?? "") + "]"
                    + (add.section ?? "") + "]" + (chief ?? "") + "]" + (add.phone ?? "") + "]" + (add.www ?? "") + "]" + (add.ppo.name ?? "") + "]" + (okatoCode) + "]"
                    + (add.ppo.oktmo.code ?? "") + "]" + (etc.ogrnData ?? "") + "]" + okveds.Substring(0, 4000) // 
                    + "]");
                }
                else
                {
                    writer.WriteLine(k++ + "]" + (init.fullName ?? "") + "]" + (con.shortName ?? "") + "]" + (con.ogrn ?? "") + "]" + (init.inn ?? "") + "]" + (init.kpp ?? "") + "]"
                    + (add.section ?? "") + "]" + (chief ?? "") + "]" + (add.phone ?? "") + "]" + (add.www ?? "") + "]" + "]" + (okatoCode) + "]"
                    + "]" + (etc.ogrnData ?? "") + "]" + okveds + "]");
                }

            }
        }
    }
}
