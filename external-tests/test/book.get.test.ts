import { assert, expect } from "chai"

import { Response, Imposter, Mountebank, Stub, EqualPredicate, HttpMethod, NotFoundResponse } from '@anev/ts-mountebank';
import { BookApiClient } from "../src/book/autorest/bookApiClient";
import * as config from '../src/config';

describe("Book - GetBook by Id", () => {

    // only runs on local machine for now
    const mb = new Mountebank();
    const bookApi = new BookApiClient({ baseUri: `http://localhost:5000` });

    before(async () => {
        let imposter = new Imposter().withPort(config.getStatsApiPort()).withStub(
            new Stub()
                .withPredicate(new EqualPredicate()
                    .withMethod(HttpMethod.GET)
                    .withPath(`/Stats/1`))
                .withResponse(new Response()
                    .withStatusCode(200)
                    .withJSONBody({ bookId: 1, copiesSold: 2405 }))
        )
            .withStub(new Stub().withResponse(new NotFoundResponse()));
            await mb.createImposter(imposter);
    })

    it('returns a book with stats if stats are available', async () => {
        const bookWithStats = await bookApi.books.get(1);
        expect(bookWithStats.copiesSold).to.equal(2405);
    })

    it('returns a book without stats if no stats are available', async () => {
        const book = await bookApi.books.get(2);
        expect(book.copiesSold).to.equal(null);
    })

    it('returns a 404 response if the book does not exist', async () => {
        try {
            await bookApi.books.get(100);
        }
        catch(error) {
            expect(error.statusCode).to.equal(404);
            return;
        }
        assert.fail('a 404 error was expected');
    })
});
