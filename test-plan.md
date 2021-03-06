### Содержание
  1. [Введение](#1)
  2. [Объект тестирования](#2)
  3. [Риски](#3)
  4. [Аспекты тестирования](#4)
  5. [Подходы к тестированию](#5)
  6. [Представление результатов](#6)
  7. [Выводы](#7)

<a name="1"></a>
### 1. Введение
  Данный файл содержит тест-план игры **Путь ниндзя**. Основной целью тестирования является
  проверка приложения на соответствие требованиям SRS.

<a name="2"></a>
### 2. Объект тестирования
Объект тестирования -  игра **Путь ниндзя**.    
Приложение обязано выполнять все заявленные функции в соответствии с SRS и удобством инспользования.

<a name="3"></a>
### 3. Риски
К рискам можно отнести следующие пункты:
* Возможна долгая загрузка при первом входе в игру.
* Возможно несвоевременное удаление объектов и другая рассинхронизация в связи с увеличением частоты кадров.

<a name="4"></a>
### 4. Аспекты тестирования
К аспектам тестирования относится реализация основных функций приложения:
* работоспособность элементов главного меню
* работоспособность элементов меню паузы
* работоспособность игровых механик
* исправно работающий внутриигровой интерфейс
* коректный переход между сценами игры

#### Функциональные требования:

##### Главное меню
Этот вариант использования небходимо протестировать на:
1. Коректное отображение GUI
2. Коректное нажатие кнопок
3. Корректный переход в подменю Опции

##### Меню паузы
Этот вариант использования небходимо протестировать на:
1. Остановка игры во время паузы
2. Коректное отображение GUI
3. Коректное нажатие кнопок
4. Коректное возобновление игры
5. Переход в главное меню.

##### Игровые механики
Этот вариант использования небходимо протестировать на:
1. Коректная работа всех механик

##### Внутриигровой интерфейс
Этот вариант использования небходимо протестировать на:
1. Коректное отображение интерфейса
2. Обновление интерфейса

##### Переход между сценами
Этот вариант использования небходимо протестировать на:
1. Коректный переход между сценами

#### Нефункциональные требования:
1. Интуитивно понятный интерфейс
2. Приятная гамма цветов в приложении  

<a name="5"></a>
### 5. Подходы к тестированию
Каждый аспект тестирования был произведен с помощью системного тестирования.  
Системное тестирование - это тестирование программы в целом.  
Каждый тест производится вручную.  

<a name="6"></a>
### 6. Представление результатов
Результаты тестирования представлены в [таблице](test_result.md).

<a name="7"></a>
### 7. Выводы
Данный тестовый план позволяет протестировать основной функционал приложения.  
Успешное прохождение всех тестов может свидетельствовать тому, что приложение  
соответствует всем заявленным требованиям и стабильно работает.
