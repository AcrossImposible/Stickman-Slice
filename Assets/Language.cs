using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{


	public static void SetLanguage(TMPro.TMP_Text text)
	{
		if (Application.systemLanguage == SystemLanguage.English)
			return;
		if (Application.systemLanguage != SystemLanguage.Russian)
			return;


		switch (text.text)
		{
			case "Get Ready! Wave is Approaching ":
				{
					text.text = "Приготовься! Приближается волна";
					break;
				}

			case "Sliced ghosts have Risen!":
				{
					text.text = "Убитые призраки Воскресли!";
					break;
				}
			case "GAME OVER":
				{
					text.text = "ИГРА ОКОНЧЕНА";
					break;
				}
			case "Blades":
				{
					text.text = "Клинки";
					break;
				}
			case "Meat Boards":
				{
					text.text = "Мясные Доски";
					break;
				}
			case "Kill 10 Stickman in one game":
				{
					text.text = "Нарежь 10 стикменов за одну игру";
					break;
				}
			case "GO!":
				{
					text.text = "ГО!";
					break;
				}
			case "Slice off 5 heads":
				{
					text.text = "Отрежьте 5 бошек";
					break;
				}
			case "Save the life of 5 Stickman":
				{
					text.text = "Сохраните жизнь 5 стикменам";
					break;
				}
			case "Earn 100 score":
				{
					text.text = "Заработайте 100 очков";
					break;
				}
			case "Slice 15 Stickman in one game":
				{
					text.text = "Нарежь 15 стикменов за одну игру";
					break;
				}
			case "Play 3 times":
				{
					text.text = "Сыграйте 3 раза";
					break;
				}
			case "Save the lives of 8 mens. We love number 8 :)":
				{
					text.text = "Сохраните жизнь 8 чувакам. Мы любим число 8 :)";
					break;
				}
			case "Cut 5 heads of the first blade":
				{
					text.text = "Отбанань 5 бошек первым клинком";
					break;
				}
			case "Achieve level 3!":
				{
					text.text = "Достигни 3 уровня!";
					break;
				}
			case "Earn 150 score":
				{
					text.text = "Заработайте 150 очков";
					break;
				}
			case "Slice 30 Stickman in one game":
				{
					text.text = "Нарежь 30 стикменов за одну игру";
					break;
				}
			case "Slice 30 hands":
				{
					text.text = "Отрежь 30 рук этим рожам";
					break;
				}
			case "Earn 888 experience points. So, again the number 8 :)":
				{
					text.text = "Заработайте 888 очков опыта. И да, снова число 8 :)";
					break;
				}
			case "Use the saw 3 times":
				{
					text.text = "Используйте пилы 3 раза";
					break;
				}
			case "The First Blade":
				{
					text.text = "Первый клинок";
					break;
				}
			case "Green Double":
				{
					text.text = "Удваиватель";
					break;
				}
			case "Slicer":
				{
					text.text = "Нарезатель";
					break;
				}
			case "A simple blade. One slice - one part of stickman":
				{
					text.text = "Простой клинок. Один срез - одна часть стикмена";
					break;
				}
			case "This blade gives a doubled scores":
				{
					text.text = "Этот клинок даёт удвоенный счет";
					break;
				}
			case "With this blade you can cut off 3 part of stickman!":
				{
					text.text = "С помощью этого лезвия вы можете отрезать 3 части стикмена!";
					break;
				}
			case "Required Level\n-= 3 =-":
				{
					text.text = "Требуется уровень\n-= 3 =-";
					break;
				}
			case "Required Level\n-= 2 =-":
				{
					text.text = "Требуется уровень\n-= 2 =-";
					break;
				}

			case "Required Level\n-= 4 =-":
				{
					text.text = "Требуется уровень\n-= 4 =-";
					break;
				}
			case "Slicing Board":
				{
					text.text = "Разделочная Доска";
					break;
				}
			case "Board Coins":
				{
					text.text = "Монеточная Доска";
					break;
				}
			case "Cave":
				{
					text.text = "Пещера";
					break;
				}
			case "Your first slicing Board":
				{
					text.text = "Ваша первая разделочная доска";
					break;
				}
			case "This Board gives +5 coins at the end of the game":
				{
					text.text = "Эта доска будет приносить +5 монеток в конце игры";
					break;
				}
			case "Here will fall the cave stakes on stickmans. This Board will help you":
				{
					text.text = "Пещерные колья будут яростно падать и мочить стикменов, тем самым помогая вам";
					break;
				}
			case "Earn 188 score. Again number 8 :)":
				{
					text.text = "Заработайте 188 очков. Ага, снова число 8 :)";
					break;
				}
			case "Slice off 15 heads":
				{
					text.text = "А теперь, отрежьте 15 бошек";
					break;
				}
			case "Slice 70 Stickman in one game":
				{
					text.text = "Замочи 70 стикменов за одну игру";
					break;
				}
			case "Save the lives of 15 stickman":
				{
					text.text = "Сохраните жизнь 15 типам";
					break;
				}
			case "Achieve level 5!":
				{
					text.text = "Получите 5 уровень!";
					break;
				}
			case "Earn 1500 experience points":
				{
					text.text = "Получи 1500 очков опыта";
					break;
				}
			case "You got +1 the Circular Saw per view ads!":
				{
					text.text = "За просмотр рекламы вы получили +1 Циркулярную Пилу!";
					break;
				}
			case "Earn 200 score":
				{
					text.text = "Заработайте 200 очков";
					break;
				}
			case "Play 5 games":
				{
					text.text = "Сыграйте 5 игр";
					break;
				}
			case "Use the saw 5 times":
				{
					text.text = "Используйте пилы 5 раз";
					break;
				}
			case "This is a beta version v0.1.6 of our new game Stickman Slice. This is the first version of the game and we'll improve it and create updates. The game will be added new blades, boards, task. Write reviews in your wishes. Thank you for downloading! :)":
				{
					text.text = "Это бета версия v0.1.6 нашей новой игры Stickman Slice. Это очень ранняя версия игры и мы будем её улучшать и создавать обновления. В игру будут добавляться новые клинки, доски, задания. Пишите в отзывах ваши пожелания. Спасибо за загрузку! :)";
					break;
				}
			case "Color Blood":
				{
					text.text = "Цвет Крови";
					break;
				}
			case "PAUSE":
				{
					text.text = "ПАУЗА";
					break;
				}
			case "Farter":
				{
					text.text = "Перделка";
					break;
				}
			case "The blade infects a defilement stickman with a probability of 35%. +5 Points for infecting":
				{
					text.text = "С вероятность 35% клинок заразит стикмена скверной. +5 Очков за заражение";
					break;
				}
			case "Slice 5 blue stickman":
				{
					text.text = "Нарежь 5 синих стикменов";
					break;
				}
			case "Save life of 5 Stickman-Ghosts":
				{
					text.text = "Сохрани жизнь 5-ти Стикменам-призракам";
					break;
				}
			case "You must survive wave of stickman":
				{
					text.text = "Тебе надо пережить волну стикменов";
					break;
				}
			case "Earn 300 score":
				{
					text.text = "Заработайте 300 очков";
					break;
				}
			case "Earn 1800 experience points":
				{
					text.text = "Получи 1800 очков опыта";
					break;
				}
			case "Slice 88 Stickman in one game":
				{
					text.text = "Замочи 88 стикменов за одну игру";
					break;
				}
			case "Slice 50 hands":
				{
					text.text = "Отрежь 50 рук этим рожам";
					break;
				}
			case "Use the saw 8 times":
				{
					text.text = "Используйте пилы 8 раз";
					break;
				}
			case "Cut off 30 heads of the Slicer-blade":
				{
					text.text = "Отрежьте 30 бошек клинком Нарезателем";
					break;
				}
			case "Privacy Policy":
				{
					text.text = "Политика Конфиденциальности";
					break;
				}
			case "Board Bonus +5":
                {
					text.text = "Бонус Доски +5";
					break;
                }

		}


	}
}
