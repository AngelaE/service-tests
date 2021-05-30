import { Imposter, Mountebank, Stub, DebugProxy } from "@anev/ts-mountebank";
import * as config from '../config';

createDebugProxy().then(r => console.log('proxy created'));

export async function createDebugProxy() {
    const mb = new Mountebank();
    const proxy = new Imposter()
        .withPort(config.getProxyPort())
        .withName('Debug proxy')
        .withStub(new Stub().withProxy(new DebugProxy(`http://localhost:${config.getStatsApiPort()}`)))

    await mb.createImposter(proxy);
}
