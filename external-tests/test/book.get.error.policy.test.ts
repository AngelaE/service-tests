import { assert, expect } from "chai"

import { Response, Imposter, Mountebank, Stub } from '@anev/ts-mountebank';
import { BookApiClient } from "../src/book/autorest/bookApiClient";
import * as config from '../src/config';
import { RequestError } from "../src/request-error";
import { Stats } from "../src/bookstats/";

describe("Book - Stats API returns Transient Error", () => {

    // only runs on local machine for now
    const mb = new Mountebank();
    const bookApi = new BookApiClient({ baseUri: `http://localhost:5000` });
    const stats: Stats = {bookId: 1, copiesSold: 555};

    before(async () => {
        let imposter = new Imposter().withPort(config.getStatsApiPort()).withStub(
            new Stub()
                .withResponse(new Response()
                    .withStatusCode(500)
                    .withJSONBody(new RequestError('Internal Server Error', 500)))
                .withResponse(new Response()
                    .withStatusCode(200)
                    .withJSONBody(stats))

        );

        try {
            await mb.createImposter(imposter);
        }
        catch (error) {
            console.log(error);
            assert.fail('The mock response could not be created');
        }
    })

    it('Book API retries getting stats on transient error and succeeds', async () => {
        const book = await bookApi.books.get(1);
        expect(book.copiesSold).to.equal(555);
    })
});
