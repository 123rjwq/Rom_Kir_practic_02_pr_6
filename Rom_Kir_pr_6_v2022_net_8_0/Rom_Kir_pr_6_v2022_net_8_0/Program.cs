namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация переменных для управления состоянием игры и ресурсами героя и босса
            bool isGameProcess = true; // Флаг для продолжения игры
            bool isIceShieldActive = false; // Флаг активности Леденеющего  щита
            bool isFireballActive = false; // Флаг активности Огненного шара
            int heroHp = 350; // Здоровье героя
            int bossHp = 400; // Здоровье босса
            int heroMana = 250; // Мана героя
            int bossMana = 250; // Мана босса
            int bossDamage = 0; // Урон, который босс наносит герою
            int sideEffect = 0; // Побочный урон от использования Огненного шара
            int heroDamage = 45; // Урон, который герой наносит боссу
            int fireballDamage = 50; // Урон от Огненного шара
            int fireballDamagex2 = 100; // Удвоенный урон от Огенного шара из-за активного щита
            int iceShieldDuration = 2; // Длительность Леденеющего  щита
            int FireballDuration =  3; // Длительность запрета на активацию щита
            int manaRecoveryAmount = 35; // Количество маны, восстанавливаемой героем
            int healthRecoveryAmount = 0; // Количество здоровья, восстанавливаемого героем
            var rand = new Random(); // Генератор случайных чисел для определения урона

            // Основной цикл игры
            while (isGameProcess)
            {
                // Вывод текущего состояния героя и босса
                Console.WriteLine("Здоровье героя: " + heroHp + "/350");
                Console.WriteLine("Мана героя: " + heroMana + "/250");
                Console.WriteLine("\nЗдоровье босса: " + bossHp + "/400");
                Console.WriteLine("Мана босса: " + bossMana + "/250");

                // Вывод доступных заклинаний героя
                Console.WriteLine("\nГерой может использовать   5 заклинаний:");
                Console.WriteLine("[1] Простая атака");
                Console.WriteLine("[2] Огненный шар - наносит " + fireballDamage + " урона.");
                Console.WriteLine("[3] Леденеющий   щит - снижает урон босса на   50% на " + iceShieldDuration + " ходов.");
                Console.WriteLine("[4] Восстановление маны - восстанавливает " + manaRecoveryAmount + " маны.");
                Console.WriteLine("[5] Восстановление здоровья - восстанавливает " + healthRecoveryAmount + " здоровья, если босс нанёс больше 10 урона герою в предыдущем ходу.\n");

                // Ввод выбранного героем заклинания
                Console.Write("Введите   номер заклинания: ");
                string spellNumber = Console.ReadLine();

                // Обработка выбранного заклинания героя
                switch (spellNumber)
                {
                    // Простая атака
                    case "1":
                        bossHp -= heroDamage; // Босс получает урон
                        Console.WriteLine("Герой использовал Простую атаку и нанёс " + heroDamage + " урона.");
                        break;

                    // Огненный шар
                    case "2":
                        isFireballActive = true; // Активация огненного шар
                        // проверка и не/использование щита
                        if (isIceShieldActive == true)
                        {
                            bossHp -= fireballDamagex2; // Босс получает усилинный урон из-за резкого перехода ото льда в огонь 
                            sideEffect = rand.Next(0, 7) * 2;
                            heroHp -= sideEffect; // Урон от Огненного шар и пара
                            heroMana -= 60; // Снижение маны после использования заклинания
                            Console.WriteLine("Герой использовал Огненный шар и нанёс " + fireballDamage + " урона и потратил 60 маны.");
                            Console.WriteLine("Герой был ошпарен и обожжен на: "+ sideEffect + " урона");
                            isIceShieldActive = false;
                            Console.WriteLine("Щит снят");
                            break;
                        }
                        else 
                        {
                            sideEffect = rand.Next(0, 7);
                            heroHp -= sideEffect; // Урон от Огненного шара
                            Console.WriteLine("Герой был обожжен на: " + sideEffect + " урона");
                            bossHp -= fireballDamage; // Босс получает урон
                            heroMana -= 60; // Снижение маны после использования заклинания
                            Console.WriteLine("Герой использовал Огненный шар и нанёс " + fireballDamage + " урона и потратил 60 маны.");
                            break;
                        }

                    // Леденеющий щит
                    case "3":
                        // Проверка можно ли использовать щит
                        if (isFireballActive == true)
                        {
                            Console.WriteLine("Вы не можете использовать Леденеющего щита после Огненный шар!");
                            break;
                        }

                        // Проверка на повторную активацию щита 
                        if (isIceShieldActive == true) 
                        {
                            isIceShieldActive = false; // Деактивация Леденеющего  щита
                            Console.WriteLine("Герой использовал повторно Леденеющий щит и лишился его."); 
                        }
                        else 
                        {
                            isIceShieldActive = true; // Активация Леденеющего  щита
                            Console.WriteLine("Герой использовал Леденеющий   щит и снизил урон босса на 50%.");
                        }
                        break;

                    // Восстановление маны
                    case "4":
                        // Проверка не полное ли кол-во маны
                        if (heroMana < 250)
                        {
                            heroMana += manaRecoveryAmount; // Восстановление маны
                            Console.WriteLine("Герой использовал Восстановление маны и восстановил " + manaRecoveryAmount + " маны.");
                        }
                        else 
                        {
                            Console.WriteLine("Максимум маны");
                        }
                        break;

                    // Восстановление здоровья
                    case "5":
                        // Восстановление хп, если босс нанес больше 10 хп
                        if (bossDamage>10)
                        {
                            // Проверка не полное ли хп
                            if (heroHp < 350)
                            {
                                healthRecoveryAmount = bossDamage; // сколько босс нанес, столько и восстановил
                                heroHp += healthRecoveryAmount; // Восстановление здоровья
                                Console.WriteLine("Герой использовал Восстановление здоровья и восстановил " + healthRecoveryAmount + " здоровья.");
                            }
                            else
                            {
                                Console.WriteLine("Максимум хп");
                            }
                        }
                        else
                        {
                            heroHp -= 20; // Потеря здоровья за неудачное использование
                            Console.WriteLine("Герой потерял 20 здоровья, потому что босс не нанёс урона в предыдущем ходу.");
                        }
                        break;
                    // Если ввел иное значение
                    default:
                        Console.WriteLine("Герой ошибся выбором заклинания, но босс не медлит и применяет заклинание.");
                        break;

                }

                // Босс выбирает свое заклинание
                int bossSpellNumber = rand.Next(1, 4); // Выбор заклинания босса
                switch (bossSpellNumber)
                {

                    // Простая атака Босса
                    case 1:
                        // проверка и не/использование щита
                        bossDamage = CalculateBossDamage(isIceShieldActive);
                        heroHp -= bossDamage;
                        Console.WriteLine("Босс нанес урона: " + bossDamage);
                        break;

                    // Атака с маной
                    case 2:
                        if (bossMana > 0)
                        {
                            // проверка и не/использование щита
                            bossDamage = CalculateBossDamage(isIceShieldActive);
                            if (isIceShieldActive == true)
                            {
                                bossDamage += 32; // Усиление при щите
                            }
                            else 
                            {
                                bossDamage += 26;// Усиление при выкл щите
                            }
                            heroHp -= bossDamage;
                            bossMana -= 60; // Снижение маны босса
                            Console.WriteLine("Босс потратил маны: " + 60 + " и нанес урона:" + bossDamage);
                        }
                        else { Console.WriteLine("Босс потратил всю ману"); }
                        break;

                    // Восстановление здоровья и маны
                    case 3:
                        // Проверка не полное ли хп
                        if (bossHp < 400)
                        {
                            bossHp += 30;
                            Console.WriteLine("Босс востановил хп:" + 30);
                        }
                        else
                        {
                            Console.WriteLine("Максимум хп");
                        }
                        // Проверка не полное ли кол-во маны
                        if (bossMana < 250)
                        {
                            bossMana += manaRecoveryAmount;
                            Console.WriteLine("Босс востановил маны: " + manaRecoveryAmount);
                        }
                        else
                        {
                            Console.WriteLine("Максимум маны");
                        }

                        bossDamage = 0; // обнуление урона босса, чтобы герой не мог хп восстановить, так как босс не атаковал

                        break;
                }

                // функция для расчета урона босса от состояния щита
                static int CalculateBossDamage(bool isIceShieldActive)
                {
                    int bossDamage = 40; // Обычный урон

                    if (isIceShieldActive==true)
                    {
                        bossDamage /= 2; // Урон снижается в два раза из-за льда
                    }
                    return bossDamage;
                }

                // Проверка условий победы
                if (bossHp <= 0)
                {
                    Console.WriteLine("Победа! Герой победил!");
                    isGameProcess = false; // Завершение игры
                }

                // Проверка условий поражения
                if (heroHp <= 0 || heroMana <= 0)
                {
                    Console.WriteLine("Поражение! Герой проиграл.");
                    isGameProcess = false; // Завершение игры
                }

                // Проверка условий ничьей
                if (((heroHp <= 0) || (heroMana <= 0)) && bossHp <= 0)
                {
                    Console.WriteLine("Невероятно! вы оба пали в этой битве!");
                    isGameProcess = false; // Завершение игры
                }

                // Предупреждение об условие гибели героя
                if (heroMana <= 65)
                {
                    Console.WriteLine("Предупреждение поражение скоро возможно, так как герой умрет, если маны будет меньше 1");
                }

                // Обновление состояния эффектов
                if (isIceShieldActive == true)
                {
                    iceShieldDuration-=1; // Уменьшение длительности Леденеющего  щита

                    if (iceShieldDuration <= 0)
                    {
                        isIceShieldActive = false; // Деактивация Леденеющего  щита
                        Console.WriteLine("Щит снят");
                    }
                }
                // уменьшение действия Огненного шара
                if (isFireballActive==true)
                { FireballDuration -= 1; }
                
                // деактивация флага использования Огненного шара
                if (FireballDuration <= 0)
                {
                    isFireballActive = false;
                    FireballDuration = 2;
                }

                Console.WriteLine("нажми Enter");
                // Очистка консоли для следующего хода
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
