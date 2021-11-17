Runner with complex road generation, depending on the player`s progress.

Created on Unity 2019.4.9f1

![icon](https://raw.githubusercontent.com/RomanDAnoshin/Runner/master/Assets/Sprites/Icon.png)

- [EN](#EN)
    - [The rules are simple](#The_rules_are_simple)
    - [Game](#Game)
    - [Road](#Road)
    - [Character](#Character)
    - [Camera](#Camera)
- [RU](#RU)
    - [Правила довольно просты](#The_rules_are_simple_RU)
    - [Игра](#Game_RU)
    - [Дорога](#Road_RU)
    - [Персонаж](#Character_RU)
    - [Камера](#Camera_RU)


# <a id="EN"> EN
### <a id="The_rules_are_simple"> The rules are simple
* The character runs along the road and collects coins.
* The character moves along three lines.
* The character dies if he collides with a barricade.
* When the character has died, there is an opportunity to return to the main menu or restart the race (while saving the progress).

### <a id="Game"> Game
Consists of two scenes:
* Main menu
  
![mainmenu](https://raw.githubusercontent.com/RomanDAnoshin/Runner/master/Readme%20screens/MainMenu.JPG)
> It is possible to start the game and exit.
> The player's statistics are also displayed, which can be reset:
> * Player name
> * Total amount of collected coins
> * The entire distance traveled for all attempts

* Road
  
![road](https://raw.githubusercontent.com/RomanDAnoshin/Runner/master/Readme%20screens/Road.JPG)
> Press the [up arrow] or [W] key to start.
> 
> Moving between the lines is carried out using the keys [arrow to the left], [arrow to the right] or [A], [D].
> To move quickly between the extreme lines, press the key twice.
> 
> Some player stats are displayed:
> * Collected coins
> * Distance covered in current race
> * Speed modifier
> 
> As player collect coins the speed of running, displacement between lines, animations increases.

The player`s progress is saved via PlayerPrefs as a JSON string.

Launching in the editor must be done only from the scene of the Main Menu!

### <a id="Road"> Road
* The road consists of Blocks with coins and barricades on them.
  
![roadblock](https://raw.githubusercontent.com/RomanDAnoshin/Runner/master/Readme%20screens/RoadBlock.JPG)
> * Each Block has a difficulty from 0 to 100.
> * Each block keeps track of how many coins a player could collect.
> * Indicates the availability of entry and exit on three lines.

* The road works in a similar way to a treadmill. As you move, the passed block is removed from the lower end and a new block is generated at the upper end of the track.
![automat](https://upload.wikimedia.org/wikipedia/commons/3/3d/Maquina.png)

* The road is generated according to the difficulty curve.
  
![difficultycurve](https://raw.githubusercontent.com/RomanDAnoshin/Runner/master/Readme%20screens/DifficultyCurve.JPG)
> * Where the X-axis is the value of the coins collected by the player.
> * The Y-axis is the difficulty value.
> * The graph is looped for infinity running.
> 
> The graph can be reconfigured, but it is necessary to accept that Y cannot be higher than 100 and lower than 0. If you need values outside this range, then you should also push the boundaries of the complexity of the blocks and create blocks with the necessary values.
> 
> In addition to the set complexity, the generation takes into account:
> * The number of repetitions for the Block. Take the block with the fewest repetitions.
> * Compatible with the outputs (along the lines) of the previous block on the road. This ensures the passability of the Road.

* The buffer capacity for blocks can be adjusted in the inspector.
* The speed of the road and its final increase from the progress in coins can also be adjusted in the inspector.

### <a id="Character"> Character
The speeds of displacement along the lines, animations of running and turning, as well as their final increase from progress in coins can be configured in the inspector.

### <a id="Camera"> Camera
The camera constantly follows the character.

# <a id="RU"> RU
### <a id="The_rules_are_simple_RU"> Правила довольно просты
* Персонаж бежит по дороге и собирает монеты.
* Персонаж движется вдоль трёх линий.
* Персонаж умирает, если сталкивается с баррикадой.
* Когда персонаж умер, есть возможность вернуться в главное меню или сделать рестарт забега (с сохранением прогресса).

### <a id="Game_RU"> Игра
Состоит из двух сцен:
* Главное Меню
  
![mainmenu](https://raw.githubusercontent.com/RomanDAnoshin/Runner/master/Readme%20screens/MainMenu.JPG)
> Есть возможность начать игру и выйти.
> Так же выводится статистика игрока, которую можно сбросить:
> * Имя игрока
> * Общее количество собранных монет
> * Вся пройденная дистанция за все попытки

* Дорога
  
![road](https://raw.githubusercontent.com/RomanDAnoshin/Runner/master/Readme%20screens/Road.JPG)
> Для старта необходимо нажать клавишу [стрелка вверх] или [W].
> 
> Перемещение между линиями осуществляется через клавиши [стрелка влево], [стрелка вправо] или [A], [D].
> Для быстрого перемещения между крайними линиями необходимо нажать клавишу дважды.
>  
> Выводятся некоторые показатели игрока:
> * Собранные монеты
> * Пройденная дистанция в текущем забеге
> * Модификатор скорости
> 
> По мере сбора монет увеличивается скорость бега, смещения между линиями, анимаций.

Прогресс игрока сохраняется через PlayerPrefs в виде JSON строки.

Запуск в редакторе необходимо делать только со сцены Главного Меню!

### <a id="Road_RU"> Дорога
* Состоит из Блоков, на которых выставлены монеты и баррикады.
  
![roadblock](https://raw.githubusercontent.com/RomanDAnoshin/Runner/master/Readme%20screens/RoadBlock.JPG)
> * Каждому Блоку выставлена сложность от 0 до 100.
> * В каждом блоке ведётся учет, сколько максимально игрок мог бы собрать монет.
> * Указывается доступность входа и выхода по трём линиям.

* Дорога работает по принципу, схожему с беговой дорожкой. По мере движения, из нижнего конца убирается пройденный блок и в верхний конец дорожки генерируется новый блок.
![automat](https://upload.wikimedia.org/wikipedia/commons/3/3d/Maquina.png)

* Дорога генерируется по кривой сложности.
  
![difficultycurve](https://raw.githubusercontent.com/RomanDAnoshin/Runner/master/Readme%20screens/DifficultyCurve.JPG)
> * Где осью Х выступает значение монет, собранных игроком.
> * Осью Y выступает значение сложности.
> * График зациклен для бесконечности бега.
> 
> График можно перенастроить, но необходимо принять, что Y не может быть выше 100 и ниже 0. Если необходимы значения за этим диапазоном, то так же следует раздвинуть границы у сложности блоков и создать блоки с необходимыми значениями.
> 
> Помимо выставленной сложности, при генерации учитываются:
> * Количество повторений у Блока. Берётся блок с наименьшим количеством повторений.
> * Совместимость с выходами (по линиям) предыдущего блока на дороге. Так обеспечивается проходимость Дороги.

* Вместимость буфера под блоки можно отрегулировать в инспекторе.
* Скорость дороги и финальный её прирост от прогресса в монетах так же можно отрегулировать в инспекторе.

### <a id="Character_RU"> Персонаж
Скорости смещения по линиям, анимаций бега и поворота, а так же их финальный прирост от прогресса в монетах можно настроить в инспекторе.

### <a id="Camera_RU"> Камера
Постоянно следует за персонажем.
