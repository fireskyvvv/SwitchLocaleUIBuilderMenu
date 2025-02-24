using System.Collections.Generic;
using UIBuilderCustomMenu.Editor;
using UnityEngine.UIElements;

namespace SwitchLocaleUIBuilderMenu.Editor
{
    internal class UnityLocalizationMenu
    {
        [CreateUIBuilderCustomMenu]
        private static List<CustomMenuInfo> Create()
        {
            const string menuLabel = "[Locale Switcher]";

            var availableLocales = UnityEngine.Localization.Settings.LocalizationSettings.AvailableLocales;

            if (availableLocales.Locales.Count == 0)
            {
                return new List<CustomMenuInfo>()
                {
                    new(
                        menuPath: $"{menuLabel} Available Locales are not found",
                        onClickAction: _ => { },
                        getStatus: _ => DropdownMenuAction.Status.Disabled
                    )
                };
            }

            var menuPathRoot = $"{menuLabel} Select Locale";
            var result = new List<CustomMenuInfo>();

            foreach (var locale in availableLocales.Locales)
            {
                result.Add(
                    new CustomMenuInfo(
                        $"{menuPathRoot}/{locale.LocaleName}",
                        onClickAction: _ =>
                        {
                            UnityEngine.Localization.Settings.LocalizationSettings.SelectedLocale = locale;
                        },
                        getStatus: _ => UnityEngine.Localization.Settings.LocalizationSettings.SelectedLocale == locale
                            ? DropdownMenuAction.Status.Checked
                            : DropdownMenuAction.Status.Normal
                    )
                );
            }

            return result;
        }
    }
}