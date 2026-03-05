using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Languages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;

namespace Nblity.Abp.AspNetCore.Components.Web.MudblazorTheme.Themes.Mudblazor.Toolbar;

[ExposeServices(typeof(LanguageSwitchViewModel))]
public class LanguageSwitchViewModel : IScopedDependency
{
    public ILanguagePlatformManager LanguagePlatformManager { get; }

    public ILanguageProvider LanguageProvider { get; }

    public IReadOnlyList<LanguageInfo> Languages { get; private set; } = new List<LanguageInfo>();

    public LanguageInfo CurrentLanguage { get; private set; }

    public bool HasLanguages { get; private set; }

    public event EventHandler OnInitialized;

    public LanguageSwitchViewModel(ILanguagePlatformManager languagePlatformManager, ILanguageProvider languageProvider)
    {
        LanguagePlatformManager = languagePlatformManager;
        LanguageProvider = languageProvider;
        
        InitializeAsync();
    }

    public virtual async void InitializeAsync()
    {
        Languages = await LanguageProvider.GetLanguagesAsync();
        CurrentLanguage = await LanguagePlatformManager.GetCurrentAsync();

        HasLanguages = Languages.Any() || CurrentLanguage == null;

        OnInitialized?.Invoke(this, EventArgs.Empty);
    }
}
