namespace SignalPlus
{
    public class Constants
    {
        public const string AdministratorRoleName = "Administrator";
    }

    /// <summary>
    /// Simple container for a single FAQ question & answer
    /// </summary>
    public class FaqItem
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    /// <summary>
    /// All of your site’s FAQs in one spot.
    /// </summary>
    public static class FaqConstants
    {
        public static readonly List<FaqItem> All =
            new List<FaqItem>
            {
                new FaqItem {
                    Question = "1. Как работи системата за сигнали “Сигнал+” на Община Якоруда?",
                    Answer   = "Системата “Сигнал+” позволява на гражданите да подават сигнали за нередности в общината чрез уеб платформата."
                },
                new FaqItem {
                    Question = "2. Как мога да подам сигнал?",
                    Answer   = "За да подадете сигнал, натиснете бутона “Подай сигнал”, попълнете необходимите данни и изпратете формуляра."
                },
                new FaqItem {
                    Question = "3. Кой може да подава сигнали, въпроси и предложения?",
                    Answer   = "Всеки гражданин, независимо дали е жител на общината или не, може да подаде сигнал."
                },
                new FaqItem {
                    Question = "4. Кой отговаря за решаването на сигналите?",
                    Answer   = "Община Якоруда отговаря за проверката и предприемането на мерки по подадените сигнали."
                },
                new FaqItem {
                    Question = "5. Мога ли да подам сигнал за нередност, ако не съм жител на общината?",
                    Answer   = "Да, системата приема сигнали от всички граждани, независимо от местожителството им."
                },
                new FaqItem {
                    Question = "6. Колко време отнема да се извърши проверка на дадения сигнал?",
                    Answer   = "Времето за проверка зависи от сложността на случая, но средно отнема между 3 и 7 работни дни."
                }
            };
    }
}
