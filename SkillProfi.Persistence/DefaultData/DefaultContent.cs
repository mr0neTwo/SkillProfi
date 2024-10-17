using SkillProfi.Domain;

namespace SkillProfi.Persistence.DefaultData;

public static class DefaultContent
{
    public static User[] Users()
    {
        User[] users =
        [
            new User
            {
                CreationDate = new DateTime(2024, 10, 1),
                Name = "Admin",
                Email = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 10, 2),
                Name = "Bob Smith",
                Email = "bob.smith@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 10, 3),
                Name = "Charlie Brown",
                Email = "charlie.brown@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 10, 4),
                Name = "Diana Prince",
                Email = "diana.prince@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 10, 5),
                Name = "Edward Blake",
                Email = "edward.blake@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 10, 6),
                Name = "Fiona Gallagher",
                Email = "fiona.gallagher@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 10, 7),
                Name = "George Washington",
                Email = "george.washington@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 10, 8),
                Name = "Helen Clark",
                Email = "helen.clark@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 10, 9),
                Name = "Ian Malcolm",
                Email = "ian.malcolm@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 10, 10),
                Name = "Julia Roberts",
                Email = "julia.roberts@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            }
        ];

        return users;
    }

    public static ClientRequest[] ClientRequests()
    {
        ClientRequest[] clientRequests =
        [
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 1),
                ClientName = "Джон Доу",
                ClientEmail = "john.doe@example.com",
                Message = "Я обращаюсь к вам, так как у меня возникли проблемы с доступом к моему аккаунту. Я пытался сбросить пароль, но не получил никаких писем.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 2),
                ClientName = "Джейн Смит",
                ClientEmail = "jane.smith@example.com",
                Message = "У меня вопрос по поводу моего недавнего заказа. Я хочу убедиться, что товар отправлен по правильному адресу.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 3),
                ClientName = "Элис Джонсон",
                ClientEmail = "alice.johnson@example.com",
                Message = "Я получила свою посылку сегодня, но она повреждена. Я хотела бы запросить замену или возврат как можно скорее.",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 4),
                ClientName = "Боб Браун",
                ClientEmail = "bob.brown@example.com",
                Message = "Можете помочь мне понять, почему моя подписка была отменена? Я не запрашивал отмену и хотел бы возобновить услугу.",
                Status = ClientRequestStatus.Denied
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 5),
                ClientName = "Чарли Дэвис",
                ClientEmail = "charlie.davis@example.com",
                Message = "Я хотел бы изменить адрес доставки для моего текущего заказа. Пожалуйста, обновите адрес, чтобы гарантировать своевременную доставку.",
                Status = ClientRequestStatus.Canceled
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 6),
                ClientName = "Диана Эванс",
                ClientEmail = "diana.evans@example.com",
                Message = "Как я могу обновить свою платежную информацию? Мои данные кредитной карты изменились, и я хочу убедиться, что будущие платежи будут обработаны правильно.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 7),
                ClientName = "Эдвард Фостер",
                ClientEmail = "edward.foster@example.com",
                Message = "У меня возникли проблемы с функцией входа. Каждый раз, когда я пытаюсь войти, появляется сообщение об ошибке, что мои учетные данные недействительны.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 8),
                ClientName = "Фиона Грин",
                ClientEmail = "fiona.green@example.com",
                Message = "Мой заказ прибыл с задержкой, и упаковка была в плохом состоянии. Могу ли я получить обновление статуса по процессу возврата и объяснение задержки?",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 9),
                ClientName = "Джордж Харрис",
                ClientEmail = "george.harris@example.com",
                Message = "Мне нужна помощь с отменой услуги. Я пытался сделать это через приложение, но столкнулся с несколькими ошибками, и ничего не получилось.",
                Status = ClientRequestStatus.Denied
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 10),
                ClientName = "Хелен Ингрэм",
                ClientEmail = "helen.ingram@example.com",
                Message = "У меня есть несколько замечаний по поводу последнего обновления вашего приложения. Новые функции кажутся более проблематичными, чем улучшениями.",
                Status = ClientRequestStatus.Canceled
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 11),
                ClientName = "Иэн Дженкинс",
                ClientEmail = "ian.jenkins@example.com",
                Message = "Можете ли вы предоставить больше информации о том, как отслеживать мою посылку? Ссылка для отслеживания, которую я получил, кажется неработающей.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 12),
                ClientName = "Джулия Кинг",
                ClientEmail = "julia.king@example.com",
                Message = "Мне нужно обновить контактную информацию в моем аккаунте. Номер телефона, указанный в данный момент, устарел и требует замены.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 13),
                ClientName = "Кевин Ли",
                ClientEmail = "kevin.lee@example.com",
                Message = "У меня возникла проблема с последней покупкой. Товар, который я получил, не соответствует заказанному, и я хотел бы организовать обмен.",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 14),
                ClientName = "Лаура Мартинез",
                ClientEmail = "laura.martinez@example.com",
                Message = "Возникла проблема с моим последним заказом. Я заказала три товара, но был доставлен только два. Мне нужна помощь в решении этого вопроса.",
                Status = ClientRequestStatus.Denied
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 15),
                ClientName = "Майкл Нельсон",
                ClientEmail = "michael.nelson@example.com",
                Message = "Я хотел бы узнать статус моего возврата. Я подал запрос две недели назад, но до сих пор не получил обновления.",
                Status = ClientRequestStatus.Canceled
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 16),
                ClientName = "Нэнси О'Брайен",
                ClientEmail = "nancy.obrien@example.com",
                Message = "Пожалуйста, помогите мне со сбросом пароля. Я следовала инструкциям, но не получила письмо для сброса.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 17),
                ClientName = "Оскар Питерс",
                ClientEmail = "oscar.peters@example.com",
                Message = "У меня возникли трудности с доступом к аккаунту после недавнего обновления. Появляется сообщение об ошибке системы.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 18),
                ClientName = "Памела Куинн",
                ClientEmail = "pamela.quinn@example.com",
                Message = "Мне нужна помощь с изменением предпочтений доставки. Я хочу выбрать другой метод доставки для моего предстоящего заказа.",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 19),
                ClientName = "Квентин Робертс",
                ClientEmail = "quentin.roberts@example.com",
                Message = "Я хочу отменить подписку, но не могу этого сделать через ваш сайт. Можете помочь мне с процессом отмены?",
                Status = ClientRequestStatus.Denied
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 20),
                ClientName = "Рэйчел Скотт",
                ClientEmail = "rachel.scott@example.com",
                Message = "Недавно я обновила свой адрес электронной почты, но все еще получаю уведомления на старую почту. Пожалуйста, обновите ваши записи.",
                Status = ClientRequestStatus.Canceled
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 21),
                ClientName = "Стив Тернер",
                ClientEmail = "steve.turner@example.com",
                Message = "Мне нужна помощь с изменением заказа. Я случайно выбрал неправильный товар и хочу исправить заказ до его отправки.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 22),
                ClientName = "Тина Андервуд",
                ClientEmail = "tina.underwood@example.com",
                Message = "Мой аккаунт заблокирован после нескольких неудачных попыток входа. Пожалуйста, помогите мне восстановить доступ.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 23),
                ClientName = "Урсула Венс",
                ClientEmail = "ursula.vance@example.com",
                Message = "Я хочу узнать больше о вашей программе лояльности и как я могу получить бонусы за покупки.",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 10, 24),
                ClientName = "Виктор Уэст",
                ClientEmail = "victor.west@example.com",
                Message = "Мне нужна помощь с активацией учетной записи. Я недавно зарегистрировался, но так и не получил письмо с подтверждением.",
                Status = ClientRequestStatus.Denied
            }
        ];
        
        return clientRequests;
    }

    public static Service[] Services()
    {
        Service[] services = 
        [
            new Service()
            {
                CreationDate = new DateTime(2024, 10, 1),
                Title = "Аудит и оптимизация IT-инфраструктуры",
                Description = "Аудит и оптимизация IT-инфраструктуры позволяет оценить состояние и эффективность использования IT-ресурсов вашей компании. В рамках этой услуги проводится комплексная проверка всех аспектов инфраструктуры: серверное оборудование, системы хранения данных, сеть, рабочие станции, программное обеспечение и безопасность.\n\nКлючевая цель аудита — выявить уязвимости, неэффективные процессы и возможности для улучшений. Это может включать выявление устаревшего оборудования, неэффективных процессов управления IT-ресурсами, излишнюю нагрузку на серверы или сбои в сетевых соединениях.\n\nПосле анализа предоставляется подробный отчет, содержащий рекомендации по оптимизации. Например, можно предложить замену устаревшего оборудования, модернизацию серверных систем или миграцию в облачные решения. Услуга также включает рекомендации по улучшению кибербезопасности, чтобы минимизировать риски, связанные с угрозами извне.\n\nЭтот процесс помогает компаниям снизить затраты на IT-поддержку, улучшить производительность сотрудников и повысить уровень безопасности. Оптимизация IT-инфраструктуры также может ускорить доступ к данным и обеспечить устойчивость бизнеса в условиях цифровой трансформации."
            },
            new Service()
            {
                CreationDate = new DateTime(2024, 10, 2),
                Title = "Разработка IT-стратегии",
                Description = "Услуга разработки IT-стратегии направлена на создание долгосрочного плана по использованию информационных технологий в бизнесе. В рамках этой услуги консультанты совместно с руководством компании анализируют текущее состояние технологий и бизнес-процессов, оценивают потребности бизнеса и разрабатывают комплексную стратегию развития.\n\nIT-стратегия охватывает такие аспекты, как выбор технологических платформ, интеграция новых систем, управление данными, кибербезопасность, внедрение инноваций и развитие кадровых IT-ресурсов. Основная цель — обеспечить, чтобы информационные технологии эффективно поддерживали текущие и будущие бизнес-задачи компании.\n\nПримером стратегии может быть внедрение облачных технологий, автоматизация бизнес-процессов, переход на использование современных программных продуктов или разработка внутренних цифровых сервисов. Стратегия также учитывает вопросы управления IT-бюджетами и эффективности инвестиций в IT-системы.\n\nРезультатом разработки IT-стратегии является четкий план, который позволяет компании адаптироваться к изменяющимся условиям рынка, обеспечить конкурентоспособность и повысить производительность."
            },
            new Service()
            {
                CreationDate = new DateTime(2024, 10, 3),
                Title = "Внедрение и настройка ERP-систем",
                Description = "Внедрение и настройка ERP-систем — это комплекс услуг по установке и интеграции программных решений, предназначенных для управления ключевыми бизнес-процессами в организации. ERP (Enterprise Resource Planning) системы помогают автоматизировать такие процессы, как управление финансами, производством, логистикой, закупками, продажами и персоналом.\n\nКонсалтинговые услуги включают анализ текущих бизнес-процессов, подбор наиболее подходящей ERP-платформы, установку программного обеспечения и его настройку под специфические потребности компании. Специалисты также проводят обучение сотрудников и тестирование системы для проверки ее работы.\n\nВнедрение ERP-системы позволяет улучшить прозрачность и эффективность процессов, сократить операционные издержки, повысить производительность и улучшить управление ресурсами. Например, система может автоматически формировать отчеты о продажах, управлять запасами и отслеживать расходы в режиме реального времени. Это дает возможность руководству компании принимать более взвешенные решения на основе достоверных данных.\n\nКроме того, ERP-системы могут интегрироваться с другими IT-системами в компании, создавая единую цифровую экосистему, что улучшает взаимодействие между различными отделами и минимизирует риски потери информации."
            },
            new Service()
            {
                CreationDate = new DateTime(2024, 10, 4),
                Title = "Кибербезопасность и защита данных",
                Description = "Консалтинг в области кибербезопасности и защиты данных — это комплекс услуг, направленных на защиту информационных систем и данных компании от внешних и внутренних угроз. Специалисты по кибербезопасности проводят аудит существующих систем, выявляют уязвимости и предлагают решения для их устранения.\n\nЭта услуга включает внедрение различных мер безопасности, таких как системы обнаружения и предотвращения атак, защита сетевой инфраструктуры, шифрование данных, контроль доступа, обучение сотрудников и внедрение передовых стандартов безопасности (например, ISO 27001).\n\nОдна из ключевых задач — минимизация рисков потери данных и предотвращение кибератак, включая фишинг, взломы и вирусные атаки. Например, внедрение многофакторной аутентификации (MFA) помогает защитить учетные записи сотрудников, а резервное копирование данных гарантирует, что информация не будет потеряна даже в случае инцидентов.\n\nЭффективная киберзащита не только предотвращает финансовые убытки, но и повышает доверие клиентов и партнеров. Регулярное тестирование и обновление систем безопасности обеспечивают компании высокую степень защиты в условиях постоянно меняющихся угроз."
            },
            new Service()
            {
                CreationDate = new DateTime(2024, 10, 5),
                Title = "Миграция в облачные сервисы",
                Description = "Миграция в облачные сервисы — это процесс переноса IT-инфраструктуры, данных и приложений компании в облачные среды, такие как Amazon Web Services (AWS), Microsoft Azure или Google Cloud. Эта услуга позволяет значительно повысить гибкость и масштабируемость IT-систем, а также сократить затраты на обслуживание и модернизацию оборудования.\n\nМиграция может включать перенос серверов, баз данных, приложений и корпоративных сервисов. Консультанты проводят оценку готовности компании к переходу в облако, разрабатывают план миграции и помогают с интеграцией новых решений в существующую инфраструктуру.\n\nОблачные решения обеспечивают высокую доступность и отказоустойчивость сервисов, что особенно важно для компаний с большим объемом данных или требующих постоянного доступа к приложениям. Например, с помощью облачных решений можно автоматизировать резервное копирование, что минимизирует риски потери данных в случае сбоев.\n\nПереход в облако также открывает возможности для внедрения инноваций, таких как контейнеризация приложений, автоматизация DevOps-процессов и использование искусственного интеллекта для анализа данных. Все это способствует повышению производительности и конкурентоспособности бизнеса."
            }
        ];

        return services;
    }

    public static Project[] Projects()
    {
        Project[] projects =
        [
            new Project
            {
                CreationDate = new DateTime(2024, 10, 1),
                Title = "Обработка кредитных заявок онлайн",
                ImageUrl = "images/1.jpg",
                Description = "Проект по созданию системы онлайн-обработки кредитных заявок для крупного банка. Основной целью проекта было автоматизировать процесс подачи и обработки заявок на кредит, уменьшив человеческое вмешательство и обеспечив более высокую скорость обслуживания клиентов. Наша команда разработала удобный и интуитивно понятный интерфейс для пользователей, который интегрировался с различными банковскими системами для верификации данных клиентов, проверки кредитной истории и одобрения кредита в автоматическом режиме. Особое внимание уделялось защите данных пользователей и соблюдению требований законодательства о персональных данных. В результате внедрения проекта банк смог сократить время обработки заявок на 60% и повысить точность прогнозирования рисков за счет использования моделей машинного обучения. Проект был выполнен в сжатые сроки и сэкономил банку значительные операционные расходы, а также улучшил клиентский опыт."
            }, 
            new Project
            {
                CreationDate = new DateTime(2024, 10, 2),
                Title = "Управление кредитами и расчет зарплаты",
                ImageUrl = "images/2.jpg",
                Description = "Данный проект был направлен на оптимизацию управления кредитами и расчета заработной платы для финансовой компании. Мы разработали систему, которая позволила автоматически рассчитывать проценты по кредитам, управлять выплатами и интегрироваться с налоговыми и бухгалтерскими системами для учета всех финансовых операций. В дополнение к кредитному модулю, система включает в себя инструменты для расчета заработной платы сотрудников с учетом налогов, премий и прочих надбавок. Основная задача проекта заключалась в создании универсальной платформы, способной масштабироваться по мере роста компании. Для этого были использованы микросервисные архитектуры и современные облачные технологии. Система позволила заказчику сократить время на обработку кредитных заявок и расчет зарплаты на 40%, а также минимизировать ошибки, возникающие при ручной обработке данных. Проект был успешно завершен с позитивной оценкой со стороны заказчика и принес значительные операционные улучшения."
            }, 
            new Project
            {
                CreationDate = new DateTime(2024, 10, 3),
                Title = "Доработка CRM и корпоративного магазина",
                ImageUrl = "images/3.jpg",
                Description = "Целью данного проекта была модернизация существующей CRM-системы и корпоративного магазина для крупной торговой компании. Клиенту требовалось улучшить взаимодействие с клиентами, оптимизировать внутренние процессы и расширить функционал интернет-магазина. В рамках проекта мы внесли изменения в CRM-систему, добавив возможность автоматической сегментации клиентов, отправки персонализированных предложений и управления программой лояльности. Кроме того, мы интегрировали CRM с корпоративным интернет-магазином, что позволило повысить эффективность продаж и улучшить обслуживание клиентов. Для интернет-магазина были разработаны новые функциональные модули для управления складом, аналитики продаж и продвижения товаров. Проект был успешно завершен с соблюдением всех сроков, и в результате клиент смог повысить конверсию интернет-магазина на 20%, а также значительно улучшить процесс обслуживания клиентов."
            }
        ];

        return projects;
    }

    public static Post[] Posts()
    {
        Post[] posts =
        [
            new Post()
            {
                CreationDate = new DateTime(2024, 10, 1),
                Title = "Открытие нового офиса в Санкт-Петербурге",
                ImageUrl = "images/4.jpg",
                Description = "Мы рады сообщить об открытии нашего нового офиса в Санкт-Петербурге. Это расширение позволит нам быть ближе к нашим клиентам и предоставлять более оперативные IT-консалтинговые услуги."
            },
            new Post()
            {
                CreationDate = new DateTime(2024, 10, 2),
                Title = "Вебинар: Будущее кибербезопасности",
                ImageUrl = "images/5.jpg",
                Description = "Приглашаем вас принять участие в нашем вебинаре, посвящённом вопросам кибербезопасности. Наши эксперты обсудят текущие угрозы и тренды в защите данных для бизнеса."
            },
            new Post()
            {
                CreationDate = new DateTime(2024, 10, 3),
                Title = "Встреча с партнёрами: Обсуждение инновационных IT-решений",
                ImageUrl = "images/6.jpg",
                Description = "Недавно состоялась встреча с нашими ключевыми партнёрами, на которой мы обсудили последние инновации в IT-индустрии и стратегию совместного развития."
            },
        ];
        
        return posts;
    }

    public static SocialMedia[] SocialMedias()
    {
        SocialMedia[] socialMedias = 
        [
            new SocialMedia
            {
                CreationDate = DateTime.Now,
                IconName = "FACEBOOK",
                Link = "https://facebook.com",
            },
            new SocialMedia
            {
                CreationDate = DateTime.Now,
                IconName = "VK",
                Link = "https://vk.ru",
            },
            new SocialMedia
            {
                CreationDate = DateTime.Now,
                IconName = "TWITTER",
                Link = "https://x.com",
            },
            new SocialMedia
            {
                CreationDate = DateTime.Now,
                IconName = "INSTAGRAM",
                Link = "https://instagram.com",
            },
            new SocialMedia
            {
                CreationDate = DateTime.Now,
                IconName = "YOUTUBE",
                Link = "https://youtube.com",
            },
            new SocialMedia
            {
                CreationDate = DateTime.Now,
                IconName = "WHATSAPP",
                Link = "https://web.whatsapp.com",
            },
            new SocialMedia
            {
                CreationDate = DateTime.Now,
                IconName = "MESSAGES",
                Link = "https://web.telegram.org/"
            }
        ];

        return socialMedias;
    }
}