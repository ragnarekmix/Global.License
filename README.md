Global.License
==============

Необходимо создать простейший сайт управления лицензиями. 
Лицензия состоит из даты создания, даты модификации, признака принадлежности конкретному пользователю и собственно ключа (любой текст). 
Так же нужно реализовать операции создания, удаления и модификации лицензии. Хранение базы пользователей с системой принадлежности один пользователь – несколько лицензий. 
Изменения должны сохраняться в xml и в базе. Выборка данных должна происходить только из одного источника, который задан в конфигурационном файле. 
Дополнительно к сайту необходимо создать REST сервис который будет предоставлять доступ ко всем перечисленным выше операциям на сайте.
Ну и логгирование в тех местах где посчитаете необходимым.

При создании проекта следует использовать ASP.NET MVC 4 или выше.
