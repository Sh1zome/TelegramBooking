import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { getBotToken } from 'nestjs-telegraf';
import { text } from 'telegraf/typings/button';
import { Markup } from 'telegraf';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  await app.listen(3000);

  const bot = app.get(getBotToken());

  bot.start((ctx) => ctx.reply('Welcome', Markup.keyboard([
    ['➡️ Send $XRP', '📈 Market'],
    ['⚖️ Balance', '⬆️ Withdraw'],
    ['🔔 Notificaiton', '👥 Contact']
  ]).resize()))
  bot.help((ctx) => ctx.reply('Send me a sticker'))
  bot.hears('hi', (ctx) => ctx.reply('Hey there'))
  bot.on(['sticker'], (ctx) => ctx.reply('👍'))

  app.use(bot.webhookCallback('/secret-path'));

}

bootstrap();
