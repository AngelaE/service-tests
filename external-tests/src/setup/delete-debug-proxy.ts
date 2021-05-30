import { Mountebank } from "@anev/ts-mountebank";
import * as config from '../config';

new Mountebank().deleteImposter(config.getProxyPort()).then(r => console.log('deleted proxy'));