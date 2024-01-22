import { HttpService } from "@nestjs/axios";
import { ForbiddenException, Get, Injectable, Logger, Post } from "@nestjs/common";
import { AxiosError, AxiosResponse } from "axios";
import { Observable, catchError, firstValueFrom, map } from "rxjs";
import { Config } from "src/config";
import * as https from 'https';

@Injectable()
export class ServerService {
  private readonly logger = new Logger(ServerService.name);
  constructor(private readonly httpService: HttpService) {}

  private config = new Config;

  @Get()
  async getUsers(): Promise<JSON>{
    var req = await firstValueFrom(this.httpService
      .get<JSON>(await this.config.getAddress() + '/users', { 
        timeout: 5000,
        httpsAgent: new https.Agent({ rejectUnauthorized: false }),
      }).pipe(
        map(response => response.data)
      ))
    return await req
  }

  @Post()
  async setUser(id: any, ctx){
    const userData: Object = {"name":id.toString()}
    await firstValueFrom(this.httpService
      .get<JSON>(await this.config.getAddress() + '/users', { 
        timeout: 5000,
        httpsAgent: new https.Agent({ rejectUnauthorized: false }),
      }).pipe(
        map(response => response.data),
        map(async (res) => {
          var empty = true 
          for(var i = 0; i < Object.values(res).length; i++) {
            if (res[i].name == id.toString()) {
              empty = false
            }
          }
          if (empty) {
            var req = await firstValueFrom(this.httpService
              .post(await this.config.getAddress() + '/users', userData, { 
                timeout: 5000,
                httpsAgent: new https.Agent({ rejectUnauthorized: false }),
              })
              .pipe(
                map(response => response.data)),
              );
        
            return req;
          }
        })
      ))
  }
  
}