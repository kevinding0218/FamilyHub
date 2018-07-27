import { HttpClient } from '@angular/common/http';
import { Resource, ResourceService } from './resource.service';
import * as moment from 'moment';
import { Moment } from 'moment';

export class Pizza extends Resource {
    // id is inherited from Resource
    name: string;
    cookedOn: Moment;
}

export class PizzaSerializer {
    fromJson(json: any): Pizza {
        const pizza = new Pizza();
        pizza.id = json.id;
        pizza.name = json.name;
        pizza.cookedOn = moment(json.cookedOn, 'mm-dd-yyyy hh:mm');

        return pizza;
    }

    toJson(pizza: Pizza): any {
        return {
            id: pizza.id,
            name: pizza.name
        };
    }
}

export class PizzaService extends ResourceService<Pizza> {
    constructor(httpClient: HttpClient) {
        super(
            httpClient,
            'http://pizzaService',
            'pizzas',
            new PizzaSerializer());
    }
}
