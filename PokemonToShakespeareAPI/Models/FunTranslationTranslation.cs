using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PokemonToShakespeareAPI
{
    public class FunTranslationTranslation
    {
        [DataContract]
        public class Success
        {

            [DataMember(Name = "total")]
            public int total { get; set; }
        }

        [DataContract]
        public class Contents
        {

            [DataMember(Name = "translated")]
            public string translated { get; set; }

            [DataMember(Name = "text")]
            public string text { get; set; }

            [DataMember(Name = "translation")]
            public string translation { get; set; }
        }

        [DataContract]
        public class TranslatedText
        {

            [DataMember(Name = "success")]
            public Success success { get; set; }

            [DataMember(Name = "contents")]
            public Contents contents { get; set; }
        }
    }
}
