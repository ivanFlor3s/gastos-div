import { Inject, Injectable } from '@angular/core';
import { MsgeError } from '@app/interfaces';
import { MSGE_ERROR_TOKEN } from '../errors.module';

@Injectable()
export class ValidationErrorMsgeService {
    constructor(@Inject(MSGE_ERROR_TOKEN) private config: MsgeError) {
        console.log('init service');
    }

    resolverErrorMessage(key: string): string | void {
        // eslint-disable-next-line no-prototype-builtins
        if (key !== 'minlength' && !this.config.hasOwnProperty(key)) {
            throw Error('No existe la propiedad en el for root del modulo');
        }
        if (key) {
            return this.config[key];
        }
    }
}
