import { AppController } from './app.controller';
import { AppService } from './app.service';
import { Module } from '@nestjs/common';
import { TelegrafModule } from 'nestjs-telegraf';

@Module({
  imports: [
    TelegrafModule.forRoot({
      token: '6543972071:AAEHHUEFZsQdWQ1v9f7erz8xQ06neMPlJ7c',
    }),
  ],
})

export class AppModule {}