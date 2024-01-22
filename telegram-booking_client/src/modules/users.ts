import { Injectable } from "@nestjs/common"
import { ServerService } from "./server"
import { NestFactory } from "@nestjs/core";
import { HttpService } from "@nestjs/axios";

@Injectable()
export class Users {
  server = new ServerService(this.httpService);
  constructor(private readonly httpService: HttpService, public ctx: any) {
    this.ctx = ctx
  }
  
  start() {
    this.server.setUser(this.ctx.message.chat.id, this.ctx)
  }

  async getUsers(): Promise<void> {
    this.ctx.reply('Users list: ' + JSON.stringify(await this.server.getUsers()))
  }

  setUser(): void {
    this.ctx
  }
}
