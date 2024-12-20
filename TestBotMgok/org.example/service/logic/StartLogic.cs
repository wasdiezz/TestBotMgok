using System.Collections;
using ConsoleAppTetsBot.org.example.Buttons;
using ConsoleAppTetsBot.org.example.EmulatorBd;
using ConsoleAppTetsBot.org.example.statemachine;

namespace ConsoleAppTetsBot.org.example.service.logic;

public class StartLogic
{
    static int currentApp = 0;

    #region старт

    public BotTextMessage ProcessWaitingCommandStart(string textFromUser, TransmittedData transmittedData)
    {
        if (textFromUser != "/start")
        {
            textFromUser =
                "Здравстуйте! Это теханическая поддержка МГОК.\n \nДанный бот призван упростить взаимодействие преподавателей и Сис админов\n \nБудем рады помочь решить проблему, которая у вас возникла\n \nДля того, чтобы бот начал работу, нажмите /start";

            return new BotTextMessage(textFromUser);
        }

        transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

        textFromUser = "Выберите то что вы хотите";

        return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
    }

    #endregion

    #region выбор действия

    public BotTextMessage ProcessWaitingQuestionsOrApplicationOrHistory(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.ShowQuestions.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SubmitApplication.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SubmitHistory.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";
            return new BotTextMessage(
                textFromUser
            );
        }

        if (textFromUser.Equals(InlineButtonsStorage.ShowQuestions.CallBackData))
        {
            transmittedData.State = State.WaitingQuestions;

            textFromUser = "Выберите, с чем возникла проблема";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemSystemShowKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.SubmitApplication.CallBackData))
        {
            transmittedData.State = State.WaitingApplication;

            textFromUser = "Пожалуйста, выберите адрес площадки.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetAddressKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.SubmitHistory.CallBackData))
        {
            EntityHistoryShowManager entityHistoryShowManager = new EntityHistoryShowManager();
            var entityHistoryShows = entityHistoryShowManager.EntityHistoryShows;

            textFromUser = "";
            
            
            
            if (entityHistoryShows.Count == 0)
            {
                textFromUser = "Ваш список заявок пуст!";
                return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackToMenuKeyboard);
            }

            transmittedData.State = State.FirstShowHistory;
            
            

             if (entityHistoryShows.Count > 0)
             {
                 for (int i = 0; i < 1; i++)
                 {
                     EntityHistoryShow entityHistoryShow = entityHistoryShows[i];
                     textFromUser += $"  ID: {entityHistoryShow.Id}\n";
                     textFromUser += $"  Статус: {entityHistoryShow.IsActive}\n";
                     textFromUser += $"  Адрес: {entityHistoryShow.AddressOfPlace}\n";
                     textFromUser += $"  Кабинет: {entityHistoryShow.NumberCabinet}\n";
                     textFromUser += $"  Телефон: {entityHistoryShow.NumberPhone}\n";
                     textFromUser += $"  Описание: {entityHistoryShow.DesciptionOfProblem}\n";
                     textFromUser += $"  Дата/Время: {entityHistoryShow.DateTime}\n";
                 }
            
                 return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetFirstShowSwitchHistory);
             }
            
            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetFirstShowSwitchHistory);
            
        }

        return null;
    }

    #endregion

    #region вопросы

    public BotTextMessage ProcessWaitingQuestions(string textFromUser, TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.ViewProblemComputer.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.ViewProblemPrinter.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.ViewProblemProjector.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.ViewProblemComputer.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemComputer;

            textFromUser =
                "Список проблем: \n1. Отсутствует подключение к сети Интернет \n2. Не включается компьютер \n3. Проблема с монитором.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFiveButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.ViewProblemPrinter.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemPrinter;

            textFromUser = "Список проблем: \n1. Не подключается к компьютеру \n2. Замятие бумаги.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFoursButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.ViewProblemProjector.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemProjector;

            textFromUser =
                "Список проблем: \n1. Не выводится изображение \n2. Проектор не включается \n3. Слишком тусклое изображение.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFiveButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Выберите то что вы хотите.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemSystemShowKeyboard);
    }

    #endregion

    #region заявка

    public BotTextMessage ProcessWaitingApplication(string textFromUser, TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstAddressPlace.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondAddressPlace.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.ThirdAddressPlace.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.FourAddressPlace.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.FiveAddressPlace.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.FirstAddressPlace.CallBackData))
        {
            transmittedData.State = State.WaitingInputCabinetNumber;

            transmittedData.DataStorage.Add("addressId", 1);
            transmittedData.DataStorage.Add("addressPlace", InlineButtonsStorage.FirstAddressPlace.Name);

            textFromUser = "Введите номер кабинета.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.SecondAddressPlace.CallBackData))
        {
            transmittedData.State = State.WaitingInputCabinetNumber;

            transmittedData.DataStorage.Add("addressId", 2);
            transmittedData.DataStorage.Add("addressPlace", InlineButtonsStorage.SecondAddressPlace.Name);

            textFromUser = "Введите номер кабинета.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.ThirdAddressPlace.CallBackData))
        {
            transmittedData.State = State.WaitingInputCabinetNumber;

            transmittedData.DataStorage.Add("addressId", 3);
            transmittedData.DataStorage.Add("addressPlace", InlineButtonsStorage.ThirdAddressPlace.Name);

            textFromUser = "Введите номер кабинета.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.FourAddressPlace.CallBackData))
        {
            transmittedData.State = State.WaitingInputCabinetNumber;

            transmittedData.DataStorage.Add("addressId", 4);
            transmittedData.DataStorage.Add("addressPlace", InlineButtonsStorage.FourAddressPlace.Name);

            textFromUser = "Введите номер кабинета.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.FiveAddressPlace.CallBackData))
        {
            transmittedData.State = State.WaitingInputCabinetNumber;

            transmittedData.DataStorage.Add("addressId", 5);
            transmittedData.DataStorage.Add("addressPlace", InlineButtonsStorage.FiveAddressPlace.Name);

            textFromUser = "Введите номер кабинета.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Выберите то что вы хотите.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemSystemShowKeyboard);
    }


    #endregion

    #region история заявок

    public BotTextMessage ProcessWaitingShowHistory(string textFromUser, TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.Next.CallBackData) &&
            (!textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData) &&
             !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData)))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";
            return new BotTextMessage(
                textFromUser
            );
        }
         if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
             transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;
        
             textFromUser = "Выберите то что вы хотите.";
             currentApp = 0;
        
             return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
         }
        
        
         if (textFromUser.Equals(InlineButtonsStorage.Next.CallBackData))
         {
             
             currentApp++;
             EntityHistoryShowManager entityHistoryShowManager = new EntityHistoryShowManager();
             var entityHistoryShows = entityHistoryShowManager.EntityHistoryShows;

             string messageText = "";
        
             if (entityHistoryShows.Count > 0)
             {
                 if (currentApp < entityHistoryShows.Count - 1)
                 {    
                     EntityHistoryShow entityHistoryShow = entityHistoryShows[currentApp];
                     messageText += $"  ID: {entityHistoryShow.Id}\n";
                     messageText += $"  Статус: {entityHistoryShow.IsActive}\n";
                     messageText += $"  Адрес: {entityHistoryShow.AddressOfPlace}\n";
                     messageText += $"  Кабинет: {entityHistoryShow.NumberCabinet}\n";
                     messageText += $"  Телефон: {entityHistoryShow.NumberPhone}\n";
                     messageText += $"  Описание: {entityHistoryShow.DesciptionOfProblem}\n";
                     messageText += $"  Дата/Время: {entityHistoryShow.DateTime}\n";
                     messageText += "\n";
                 }
                 else if (currentApp == entityHistoryShows.Count - 1)
                 {
                     EntityHistoryShow entityHistoryShow = entityHistoryShows[currentApp];
                     messageText += $"  ID: {entityHistoryShow.Id}\n";
                     messageText += $"  Статус: {entityHistoryShow.IsActive}\n";
                     messageText += $"  Адрес: {entityHistoryShow.AddressOfPlace}\n";
                     messageText += $"  Кабинет: {entityHistoryShow.NumberCabinet}\n";
                     messageText += $"  Телефон: {entityHistoryShow.NumberPhone}\n";
                     messageText += $"  Описание: {entityHistoryShow.DesciptionOfProblem}\n";
                     messageText += $"  Дата/Время: {entityHistoryShow.DateTime}\n";
                     messageText += "\n";
                     
                     return new BotTextMessage(messageText, InlineKeyboardsStorage.GetLastShowSwitchHistory);
                 }
             }
        
             return new BotTextMessage(messageText, InlineKeyboardsStorage.GetShowSwitchHistory);
         }
        
         if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
         {
             currentApp--;
             EntityHistoryShowManager entityHistoryShowManager = new EntityHistoryShowManager();
             var entityHistoryShows = entityHistoryShowManager.EntityHistoryShows;
             textFromUser = "";
        
             if (entityHistoryShows.Count > 0)
             {
                 if (currentApp > 0)
                 {
                     EntityHistoryShow entityHistoryShow = entityHistoryShows[currentApp];
                     textFromUser += $"  ID: {entityHistoryShow.Id}\n";
                     textFromUser += $"  Статус: {entityHistoryShow.IsActive}\n";
                     textFromUser += $"  Адрес: {entityHistoryShow.AddressOfPlace}\n";
                     textFromUser += $"  Кабинет: {entityHistoryShow.NumberCabinet}\n";
                     textFromUser += $"  Телефон: {entityHistoryShow.NumberPhone}\n";
                     textFromUser += $"  Описание: {entityHistoryShow.DesciptionOfProblem}\n";
                     textFromUser += $"  Дата/Время: {entityHistoryShow.DateTime}\n";
                     textFromUser += "\n";
                     transmittedData.State = State.WaitingShowHistory;
                 }
                 else if (currentApp == 0)
                 {
                     EntityHistoryShow entityHistoryShow = entityHistoryShows[0];
                     textFromUser += $"  ID: {entityHistoryShow.Id}\n";
                     textFromUser += $"  Статус: {entityHistoryShow.IsActive}\n";
                     textFromUser += $"  Адрес: {entityHistoryShow.AddressOfPlace}\n";
                     textFromUser += $"  Кабинет: {entityHistoryShow.NumberCabinet}\n";
                     textFromUser += $"  Телефон: {entityHistoryShow.NumberPhone}\n";
                     textFromUser += $"  Описание: {entityHistoryShow.DesciptionOfProblem}\n";
                     textFromUser += $"  Дата/Время: {entityHistoryShow.DateTime}\n";
                     textFromUser += "\n";
                     transmittedData.State = State.WaitingShowHistory;
                     return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetFirstShowSwitchHistory);
                 }
             }
        
             return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetShowSwitchHistory);
         }
    
        return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetShowSwitchHistory);
    }

    #endregion
}