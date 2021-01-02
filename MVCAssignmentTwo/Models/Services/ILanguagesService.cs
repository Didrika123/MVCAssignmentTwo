using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAssignmentTwo.Models.Services
{
    public interface ILanguagesService
    {
        Language Add(LanguageViewModel createLanguage);

        LanguagesViewModel All();
        LanguagesViewModel All(int page);
        LanguagesViewModel FindBy(LanguagesViewModel search);
        LanguagesViewModel FindBy(LanguagesViewModel search, int page);
        Language FindBy(int id);
        Language LazyFindBy(int id);
        Language Edit(int id, LanguageViewModel createLanguage);
        bool Remove(int id);
    }
}
