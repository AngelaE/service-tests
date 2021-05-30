import { assert, expect } from "chai"

import { Response, Imposter, Mountebank, Stub, EqualPredicate, HttpMethod, NotFoundResponse } from '@anev/ts-mountebank';
import { Stats } from "../src/bookstats";
import { BookApiClient } from "../src/book/autorest/bookApiClient";
import * as config from '../src/config';

describe("Book - GetBook by Id", () => {

    // only runs on local machine for now
    const mb = new Mountebank();
    const bookApi = new BookApiClient({ baseUri: `http://localhost:5000` });

    before(async () => {
        let imposter = new Imposter().withPort(config.getStatsApiPort()).withStub(
            new Stub()
                .withResponse(new Response()
                    .withStatusCode(500)
                    .withJSONBody({ error: 'internal_server_error' }))
        );

        // act
        try {
            await mb.createImposter(imposter);
        }
        catch (error) {
            console.log(error);
            assert.fail('The mock response could not be created');
        }
    })

    it('returns a book without stats if no stats are available', async () => {
        const book = await bookApi.books.get(1);
        expect(book.copiesSold).to.equal(null);
    })
});
