using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org
{
    public class Organisation
    {
        public PagedContent? pagedContent { get; set; }
    }
    public class PagedContent
    {
        public List<Content>? content { get; set; }
    }

    public class Content
    {
        public Body? body { get; set; }

    }

    public class Body
    {
        public Position? position { get; set; }
    }

    public class Position
    {
        public Initiator? initiator { get; set; }
        public MainContent? main { get; set; }
        public Other? other { get; set; }
        public Additional? additional { get; set; }
    }
    public class Additional
    {
        public Ppo? ppo { get; set; }
        public InstitutionType? institutionType { get; set; }
        public List<Activity>? activity { get; set; }
        public string? phone { get; set; }
        public string? www { get; set; }
        public string? section { get; set; }



        public class InstitutionType
        {
            public string? name { get; set; }
        }

        public class Ppo
        {
            public Okato? okato { get; set; }
            public Oktmo? oktmo { get; set; }
            public string? name { get; set; }

            public class Okato
            {
                public string? code { get; set; }
            }
            public class Oktmo
            {
                public string? code { get; set; }
            }

        }
    }
    public class Activity
    {
        public Okved? okved { get; set; }

        public class Okved
        {
            public string? code { get; set; }
            public string? name { get; set; }
        }
    }
    public class Other
    {
        public List<Chief>? chief { get; set; }
        public List<Founder>? founder { get; set; }
        public string? ogrnData { get; set; }
    }
    public class Chief
    {
        public string? lastName { get; set; }
        public string? firstName { get; set; }
        public string? midddleName { get; set; }
    }
    public class Founder
    {
        public string? fullName { get; set; }
    }


    public class Initiator
    {
        public string? fullName { get; set; }
        public string? inn { get; set; }
        public string? kpp { get; set; }
    }

    public class MainContent
    {
        public string? shortName { get; set; }
        public string? ogrn { get; set; }
    }
}
