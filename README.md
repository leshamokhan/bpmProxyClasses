# bpmProxyClasses


Необходимо реализовать веб-приложение управляющее сущностями сторонней
информационной системы с помощью API сторонней системы.

* Логика приложения:

  * Логика на стороне сервера приложения:
Требуется разработать сервис, который с помощью прокси-классов будет
реализовывать логику получения всех «Контактов» сторонней системы.
Прокси классы можно сгенерировать на основе svc-сервиса
EntityDataService.svc.

    Логику CRUD (создания, изменения и удаления) контактов необходимо
реализовывать через тот же сервис, однако реализация должна быть
осуществлена с помощью http-запросов.

    Основные свойства Контакта, следуемые использовать для выполнения
домашнего задания:
    * Name
    * MobilePhone
    * Dear
    * JobTitle
    * BirthDate

  * Логика на стороне пользовательского интерфейса:
Следует разработать web-страницу, на которой будет отображаться список
Контактов, при выборе контакта должна появляться возможность изменять
значение его свойств, а также должна быть возможность создания и
удаление Контактов. Требуется реализовать валидацию введенных значений с
помощью javascript (критерии валидации придумать самостоятельно).

  Ссылка сторонней системы: http://shedko.beesender.com
Логин: Supervisor
Пароль: Supervisor

  Ссылка для прокси классов:
http://shedko.beesender.com/0/serviceModel/entitydataservice.svc

  Ссылка на SDK:
https://academy.terrasoft.ru/documents/technic-sdk/7-13/dokumentaciya-po-razrabotke
