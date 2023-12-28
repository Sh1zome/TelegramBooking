import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { getBotToken } from 'nestjs-telegraf';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  await app.listen(3000);

  // ...
  const bot = app.get(getBotToken());

  bot.start((ctx) => ctx.reply('Welcome'))
  bot.help((ctx) => ctx.reply('Send me a sticker'))
  bot.hears('hi', (ctx) => ctx.reply('Hey there'))

  app.use(bot.webhookCallback('/secret-path'));

}
bootstrap();
