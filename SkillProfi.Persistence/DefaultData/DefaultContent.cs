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
                CreationDate = new DateTime(2024, 9, 1),
                Name = "Admin",
                Email = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 9, 2),
                Name = "Bob Smith",
                Email = "bob.smith@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 9, 3),
                Name = "Charlie Brown",
                Email = "charlie.brown@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 9, 4),
                Name = "Diana Prince",
                Email = "diana.prince@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 9, 5),
                Name = "Edward Blake",
                Email = "edward.blake@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 9, 6),
                Name = "Fiona Gallagher",
                Email = "fiona.gallagher@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 9, 7),
                Name = "George Washington",
                Email = "george.washington@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 9, 8),
                Name = "Helen Clark",
                Email = "helen.clark@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 9, 9),
                Name = "Ian Malcolm",
                Email = "ian.malcolm@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456")
            },
            new User
            {
                CreationDate = new DateTime(2024, 9, 10),
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
                CreationDate = new DateTime(2024, 9, 1),
                ClientName = "John Doe",
                ClientEmail = "john.doe@example.com",
                Message = "I am reaching out because I am having trouble accessing my account. I tried resetting my password but have not received any emails.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 2),
                ClientName = "Jane Smith",
                ClientEmail = "jane.smith@example.com",
                Message = "I have a question about my recent order. I want to make sure that the item is being shipped to the correct address.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 3),
                ClientName = "Alice Johnson",
                ClientEmail = "alice.johnson@example.com",
                Message = "I received my package today but it appears to be damaged. I would like to request a replacement or a refund as soon as possible.",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 4),
                ClientName = "Bob Brown",
                ClientEmail = "bob.brown@example.com",
                Message = "Can you help me understand why my subscription was canceled? I did not request any cancellation and would like to resume my service.",
                Status = ClientRequestStatus.Denied
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 5),
                ClientName = "Charlie Davis",
                ClientEmail = "charlie.davis@example.com",
                Message = "I would like to change the delivery address for my current order. Please update the address to ensure timely delivery.",
                Status = ClientRequestStatus.Canceled
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 6),
                ClientName = "Diana Evans",
                ClientEmail = "diana.evans@example.com",
                Message = "How can I update my payment information? My credit card details have changed, and I need to make sure future payments are processed correctly.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 7),
                ClientName = "Edward Foster",
                ClientEmail = "edward.foster@example.com",
                Message = "I’m experiencing issues with the login functionality. Whenever I try to log in, I get an error message stating that my credentials are invalid.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 8),
                ClientName = "Fiona Green",
                ClientEmail = "fiona.green@example.com",
                Message = "My order arrived late and the package was not in good condition. Can I get a status update on the refund process and an explanation for the delay?",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 9),
                ClientName = "George Harris",
                ClientEmail = "george.harris@example.com",
                Message = "I need assistance with cancelling a service. I tried to do it through the app but encountered several errors and it didn’t go through.",
                Status = ClientRequestStatus.Denied
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 10),
                ClientName = "Helen Ingram",
                ClientEmail = "helen.ingram@example.com",
                Message = "I have some feedback regarding the recent update to your application. The new features seem to be causing more problems than improvements.",
                Status = ClientRequestStatus.Canceled
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 11),
                ClientName = "Ian Jenkins",
                ClientEmail = "ian.jenkins@example.com",
                Message = "Can you please provide more information on how to track my package? The tracking link provided seems to be broken.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 12),
                ClientName = "Julia King",
                ClientEmail = "julia.king@example.com",
                Message = "I need to update my contact information on my account. The phone number currently listed is outdated and needs to be replaced.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 13),
                ClientName = "Kevin Lee",
                ClientEmail = "kevin.lee@example.com",
                Message = "I encountered a problem with my latest purchase. The item I received is not what I ordered, and I would like to arrange an exchange.",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 14),
                ClientName = "Laura Martinez",
                ClientEmail = "laura.martinez@example.com",
                Message = "There was an issue with my recent order. I ordered three items, but only two were delivered. I need assistance in resolving this discrepancy.",
                Status = ClientRequestStatus.Denied
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 15),
                ClientName = "Michael Nelson",
                ClientEmail = "michael.nelson@example.com",
                Message = "I would like to inquire about the status of my refund. I submitted a request two weeks ago and have not yet received an update.",
                Status = ClientRequestStatus.Canceled
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 16),
                ClientName = "Nancy O'Brien",
                ClientEmail = "nancy.obrien@example.com",
                Message = "Please assist me with resetting my password. I have followed the instructions but have not received the reset email.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 17),
                ClientName = "Oscar Peters",
                ClientEmail = "oscar.peters@example.com",
                Message = "I am having difficulty accessing my account after the recent update. It keeps showing an error message about a system malfunction.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 18),
                ClientName = "Pamela Quinn",
                ClientEmail = "pamela.quinn@example.com",
                Message = "I need help with changing my delivery preferences. I’d like to opt for a different delivery method for my upcoming order.",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 19),
                ClientName = "Quentin Roberts",
                ClientEmail = "quentin.roberts@example.com",
                Message = "I want to cancel my subscription but am having trouble doing so through your website. Could you assist me with the cancellation process?",
                Status = ClientRequestStatus.Denied
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 20),
                ClientName = "Rachel Scott",
                ClientEmail = "rachel.scott@example.com",
                Message = "I recently updated my email address, but I am still receiving notifications to my old email. Please update your records accordingly.",
                Status = ClientRequestStatus.Canceled
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 21),
                ClientName = "Steve Turner",
                ClientEmail = "steve.turner@example.com",
                Message = "I need help with modifying my order. I accidentally selected the wrong item and would like to correct the order before it is shipped.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 22),
                ClientName = "Tina Underwood",
                ClientEmail = "tina.underwood@example.com",
                Message = "My account has been locked due to multiple failed login attempts. I need assistance with unlocking my account as soon as possible.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 23),
                ClientName = "Ursula Vincent",
                ClientEmail = "ursula.vincent@example.com",
                Message = "I would like to get a detailed report of my recent transactions. Can you provide a summary of all the purchases I made in the last month?",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 24),
                ClientName = "Victor White",
                ClientEmail = "victor.white@example.com",
                Message = "I am having issues with the product warranty. The warranty was supposed to cover repairs, but I am being charged for the service.",
                Status = ClientRequestStatus.Denied
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 25),
                ClientName = "Wendy Xavier",
                ClientEmail = "wendy.xavier@example.com",
                Message = "I accidentally placed a duplicate order and need to cancel one of them. Please assist me with canceling the extra order.",
                Status = ClientRequestStatus.Canceled
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 26),
                ClientName = "Xander Young",
                ClientEmail = "xander.young@example.com",
                Message = "I am having trouble with the checkout process. Every time I try to complete my purchase, I get an error message and can’t proceed.",
                Status = ClientRequestStatus.Received
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 27),
                ClientName = "Yolanda Zane",
                ClientEmail = "yolanda.zane@example.com",
                Message = "Can you provide more details about the new features introduced in your latest app update? I would like to know how they work.",
                Status = ClientRequestStatus.AtWork
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 28),
                ClientName = "Zachary Adams",
                ClientEmail = "zachary.adams@example.com",
                Message = "I am experiencing issues with the mobile app's performance. It has been crashing frequently, and I need help resolving this issue.",
                Status = ClientRequestStatus.Done
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 29),
                ClientName = "Aiden Brown",
                ClientEmail = "aiden.brown@example.com",
                Message = "Please help me with the installation of the new software update. I am having trouble following the installation instructions.",
                Status = ClientRequestStatus.Denied
            },
            new ClientRequest
            {
                CreationDate = new DateTime(2024, 9, 30),
                ClientName = "Bella Carter",
                ClientEmail = "bella.carter@example.com",
                Message = "I have noticed some discrepancies in my recent bill. Can you provide a detailed explanation of the charges and correct any errors?",
                Status = ClientRequestStatus.Canceled
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
                Title = "Аудит и оптимизация IT-инфраструктуры",
                Description = "Аудит и оптимизация IT-инфраструктуры позволяет оценить состояние и эффективность использования IT-ресурсов вашей компании. В рамках этой услуги проводится комплексная проверка всех аспектов инфраструктуры: серверное оборудование, системы хранения данных, сеть, рабочие станции, программное обеспечение и безопасность.\n\nКлючевая цель аудита — выявить уязвимости, неэффективные процессы и возможности для улучшений. Это может включать выявление устаревшего оборудования, неэффективных процессов управления IT-ресурсами, излишнюю нагрузку на серверы или сбои в сетевых соединениях.\n\nПосле анализа предоставляется подробный отчет, содержащий рекомендации по оптимизации. Например, можно предложить замену устаревшего оборудования, модернизацию серверных систем или миграцию в облачные решения. Услуга также включает рекомендации по улучшению кибербезопасности, чтобы минимизировать риски, связанные с угрозами извне.\n\nЭтот процесс помогает компаниям снизить затраты на IT-поддержку, улучшить производительность сотрудников и повысить уровень безопасности. Оптимизация IT-инфраструктуры также может ускорить доступ к данным и обеспечить устойчивость бизнеса в условиях цифровой трансформации."
            },
            new Service()
            {
                Title = "Разработка IT-стратегии",
                Description = "Услуга разработки IT-стратегии направлена на создание долгосрочного плана по использованию информационных технологий в бизнесе. В рамках этой услуги консультанты совместно с руководством компании анализируют текущее состояние технологий и бизнес-процессов, оценивают потребности бизнеса и разрабатывают комплексную стратегию развития.\n\nIT-стратегия охватывает такие аспекты, как выбор технологических платформ, интеграция новых систем, управление данными, кибербезопасность, внедрение инноваций и развитие кадровых IT-ресурсов. Основная цель — обеспечить, чтобы информационные технологии эффективно поддерживали текущие и будущие бизнес-задачи компании.\n\nПримером стратегии может быть внедрение облачных технологий, автоматизация бизнес-процессов, переход на использование современных программных продуктов или разработка внутренних цифровых сервисов. Стратегия также учитывает вопросы управления IT-бюджетами и эффективности инвестиций в IT-системы.\n\nРезультатом разработки IT-стратегии является четкий план, который позволяет компании адаптироваться к изменяющимся условиям рынка, обеспечить конкурентоспособность и повысить производительность."
            },
            new Service()
            {
                Title = "Внедрение и настройка ERP-систем",
                Description = "Внедрение и настройка ERP-систем — это комплекс услуг по установке и интеграции программных решений, предназначенных для управления ключевыми бизнес-процессами в организации. ERP (Enterprise Resource Planning) системы помогают автоматизировать такие процессы, как управление финансами, производством, логистикой, закупками, продажами и персоналом.\n\nКонсалтинговые услуги включают анализ текущих бизнес-процессов, подбор наиболее подходящей ERP-платформы, установку программного обеспечения и его настройку под специфические потребности компании. Специалисты также проводят обучение сотрудников и тестирование системы для проверки ее работы.\n\nВнедрение ERP-системы позволяет улучшить прозрачность и эффективность процессов, сократить операционные издержки, повысить производительность и улучшить управление ресурсами. Например, система может автоматически формировать отчеты о продажах, управлять запасами и отслеживать расходы в режиме реального времени. Это дает возможность руководству компании принимать более взвешенные решения на основе достоверных данных.\n\nКроме того, ERP-системы могут интегрироваться с другими IT-системами в компании, создавая единую цифровую экосистему, что улучшает взаимодействие между различными отделами и минимизирует риски потери информации."
            },
            new Service()
            {
                Title = "Кибербезопасность и защита данных",
                Description = "Консалтинг в области кибербезопасности и защиты данных — это комплекс услуг, направленных на защиту информационных систем и данных компании от внешних и внутренних угроз. Специалисты по кибербезопасности проводят аудит существующих систем, выявляют уязвимости и предлагают решения для их устранения.\n\nЭта услуга включает внедрение различных мер безопасности, таких как системы обнаружения и предотвращения атак, защита сетевой инфраструктуры, шифрование данных, контроль доступа, обучение сотрудников и внедрение передовых стандартов безопасности (например, ISO 27001).\n\nОдна из ключевых задач — минимизация рисков потери данных и предотвращение кибератак, включая фишинг, взломы и вирусные атаки. Например, внедрение многофакторной аутентификации (MFA) помогает защитить учетные записи сотрудников, а резервное копирование данных гарантирует, что информация не будет потеряна даже в случае инцидентов.\n\nЭффективная киберзащита не только предотвращает финансовые убытки, но и повышает доверие клиентов и партнеров. Регулярное тестирование и обновление систем безопасности обеспечивают компании высокую степень защиты в условиях постоянно меняющихся угроз."
            },
            new Service()
            {
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
                Title = "Обработка кредитных заявок онлайн",
                ImageUrl = "images/1.png",
                Description = "Проект по созданию системы онлайн-обработки кредитных заявок для крупного банка. Основной целью проекта было автоматизировать процесс подачи и обработки заявок на кредит, уменьшив человеческое вмешательство и обеспечив более высокую скорость обслуживания клиентов. Наша команда разработала удобный и интуитивно понятный интерфейс для пользователей, который интегрировался с различными банковскими системами для верификации данных клиентов, проверки кредитной истории и одобрения кредита в автоматическом режиме. Особое внимание уделялось защите данных пользователей и соблюдению требований законодательства о персональных данных. В результате внедрения проекта банк смог сократить время обработки заявок на 60% и повысить точность прогнозирования рисков за счет использования моделей машинного обучения. Проект был выполнен в сжатые сроки и сэкономил банку значительные операционные расходы, а также улучшил клиентский опыт."
            }, 
            new Project
            {
                Title = "Управление кредитами и расчет зарплаты",
                ImageUrl = "images/2.jpg",
                Description = "Данный проект был направлен на оптимизацию управления кредитами и расчета заработной платы для финансовой компании. Мы разработали систему, которая позволила автоматически рассчитывать проценты по кредитам, управлять выплатами и интегрироваться с налоговыми и бухгалтерскими системами для учета всех финансовых операций. В дополнение к кредитному модулю, система включает в себя инструменты для расчета заработной платы сотрудников с учетом налогов, премий и прочих надбавок. Основная задача проекта заключалась в создании универсальной платформы, способной масштабироваться по мере роста компании. Для этого были использованы микросервисные архитектуры и современные облачные технологии. Система позволила заказчику сократить время на обработку кредитных заявок и расчет зарплаты на 40%, а также минимизировать ошибки, возникающие при ручной обработке данных. Проект был успешно завершен с позитивной оценкой со стороны заказчика и принес значительные операционные улучшения."
            }, 
            new Project
            {
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
                Title = "Открытие нового офиса в Санкт-Петербурге",
                ImageUrl = "images/4.png",
                Description = "Мы рады сообщить об открытии нашего нового офиса в Санкт-Петербурге. Это расширение позволит нам быть ближе к нашим клиентам и предоставлять более оперативные IT-консалтинговые услуги."
            },
            new Post()
            {
                Title = "Вебинар: Будущее кибербезопасности",
                ImageUrl = "images/5.png",
                Description = "Приглашаем вас принять участие в нашем вебинаре, посвящённом вопросам кибербезопасности. Наши эксперты обсудят текущие угрозы и тренды в защите данных для бизнеса."
            },
            new Post()
            {
                Title = "Встреча с партнёрами: Обсуждение инновационных IT-решений",
                ImageUrl = "images/6.png",
                Description = "Недавно состоялась встреча с нашими ключевыми партнёрами, на которой мы обсудили последние инновации в IT-индустрии и стратегию совместного развития."
            },
        ];
        
        return posts;
    }
}