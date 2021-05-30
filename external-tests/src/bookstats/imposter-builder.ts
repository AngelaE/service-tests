import { Imposter, Stub } from "@anev/ts-mountebank";

export class ImposterBuilder {
    public protocol = 'http';
    public stubs: Stub[] = [];

    constructor(public name: string = 'imposter', public port: number) {
    }

    public withStub(stub: Stub) : ImposterBuilder {
        this.stubs.push(stub);
        return this;
    }

    public create() : Imposter {
        const imposter = new Imposter()
            .withName(this.name)
            .withPort(this.port);

        this.stubs.forEach(stub => {
            imposter.withStub(stub);
        });;
        return imposter;
    }
}