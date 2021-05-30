import {EqualPredicate, HttpMethod, Imposter, Predicate, Response, Stub} from '@anev/ts-mountebank';
import { Stats } from '.';
import { RequestError } from '../request-error';
import { ImposterBuilder } from './imposter-builder';

export class StatsImposterBuilder extends ImposterBuilder {
    constructor(name: string, port: number) {
        super(name, port);
    }

    public withBookStats(bookId: number, response: Stats) : StatsImposterBuilder {
        const stub = new Stub()
            .withPredicate(this.getBookStatsPredicate(bookId))
            .withResponse(new Response()
                .withStatusCode(200)
                .withJSONBody(response));
        
        this.withStub(stub)
        return this;
    }

    public withBookStatsError(bookId: number, error: RequestError) : StatsImposterBuilder {
        const stub = new Stub()
            .withPredicate(this.getBookStatsPredicate(bookId))
            .withResponse(new Response()
                .withStatusCode(error.status)
                .withJSONBody(error));
        
        this.withStub(stub)
        return this;
    }

    public withBookStatsResponses(bookId: number, responses: (Stats|RequestError)[]) : StatsImposterBuilder {
        const stub = new Stub()
            .withPredicate(this.getBookStatsPredicate(bookId));

        responses.forEach(response => {
            const statusCode = response instanceof RequestError ? response.status : 200;
            stub.withResponse(new Response()
                .withStatusCode(statusCode)
                .withJSONBody(response));
        });
        
        this.withStub(stub)
        return this;
    }

    private getBookStatsPredicate(bookId: number) : Predicate {
        return new EqualPredicate()
            .withMethod(HttpMethod.GET)
            .withPath(`/Stats/${bookId}`);
    }
}
