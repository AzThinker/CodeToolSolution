using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableTranslatorEx.Model;
using TableTranslatorEx.Model.Settings;

namespace MetaWorkLib.Domain
{
    public class AzMetaProfile : TranslationProfile
    {
        public override string ProfileName => "AtkProfile";
        protected override void Configure()
        {
            AddTranslation<AzMetaCustomCloumEntity>(new TranslationSettings("AtkMetaCustomCloum"))
              .AddColumnConfigurationForAllMembers();
            AddTranslation<AzMetaCloumEntity>(new TranslationSettings("AtkMetaCloum"))
                         .AddColumnConfigurationForAllMembers();
        }
    }
}
