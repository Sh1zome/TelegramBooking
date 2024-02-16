import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { getBotToken } from 'nestjs-telegraf';
import { text } from 'telegraf/typings/button';
import { Markup } from 'telegraf';
import { Users } from './modules/users';
import { HttpService } from '@nestjs/axios';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  const httpService = new HttpService;
  await app.listen(3000);

  const bot = app.get(getBotToken());

  bot.start((ctx) => {
    ctx.reply('Welcome', Markup.keyboard([
      ['Get All Users', 'Get 1 User', 'Add 1 User', 'Set User', 'Delete User']
    ]).resize())
    new Users(httpService, ctx).start()
  })

  bot.hears('Get All Users', (ctx) => new Users(httpService, ctx).getUsers())
  bot.hears('Get 1 User', (ctx) => new Users(httpService, ctx).getUser())
  bot.hears('Set User', (ctx) => new Users(httpService, ctx).setUser())
  bot.hears('Add 1 User', (ctx) => new Users(httpService, ctx).addUser())
  bot.hears('Del User', (ctx) => new Users(httpService, ctx).delUser())

  bot.help((ctx) => ctx.reply('Send me a sticker'))
  bot.hears('hi', (ctx) => ctx.reply('Hey there'))
  bot.on(['sticker'], (ctx) => ctx.reply('👍'))

  app.use(bot.webhookCallback('/secret-path'));

}

bootstrap();
