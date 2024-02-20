import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { getBotToken } from 'nestjs-telegraf';
import { Users } from './modules/users';
import { HttpService } from '@nestjs/axios';
import { Markup } from 'telegraf';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  const httpService = new HttpService;
  await app.listen(3000);

  const bot = app.get(getBotToken());

  let startupKeyboard = Markup.keyboard([
    ['Get All Users', 'Get 1 User', 'Add 1 User', 'Set User', 'Delete User']
  ]).resize()
  
  let users: Users = new Users(httpService, bot)
  let Case = 'default'

  bot.start((ctx) => {
    ctx.reply('Welcome', startupKeyboard)
    users.start(ctx)
  })

  bot.hears('Get All Users', (ctx) => users.getUsers(ctx, startupKeyboard))

  bot.hears('Get 1 User', (ctx) => {
    Case = 'GetUser'
    ctx.reply('Напиши ID пользователя о котором нужно получить информацию', Markup.removeKeyboard())
  })

  bot.hears('Set User', (ctx) => { 
    Case = 'SetUser'
    ctx.reply('Напиши ID пользователя и Channel ID на который ты бы хотел его изменить в формате: `[channel_id], [id]`', Markup.removeKeyboard())
  })

  bot.hears('Add 1 User', (ctx) => {
    Case = 'AddUser'
    ctx.reply('Напиши Channel ID который ты бы хотел добавить как пользователя', Markup.removeKeyboard())
  })

  bot.hears('Delete User', (ctx) => {
    Case = 'DelUser'
    ctx.reply('Напиши ID пользователя которого ты бы хотел удалить', Markup.removeKeyboard())
  })

  bot.help((ctx) => ctx.reply('Send me a sticker'))
  bot.hears('hi', (ctx) => ctx.reply('Hey there'))
  bot.on(['sticker'], (ctx) => ctx.reply('👍'))

  bot.on('text', async ctx => {
    switch (Case) {
      case 'GetUser':
        users.getUser(ctx, startupKeyboard)
        break
      case 'SetUser':
        users.setUser(ctx, startupKeyboard)
        break
      case 'AddUser':
        users.addUser(ctx, startupKeyboard)
        break
      case 'DelUser':
        users.delUser(ctx, startupKeyboard)
        break
      default:
        break
    }
  })

  app.use(bot.webhookCallback('/secret-path'));

}

bootstrap();
