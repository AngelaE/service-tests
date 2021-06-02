import { expect } from "chai"

import { Response, Imposter, Mountebank, Stub } from '@anev/ts-mountebank';
import { BookApiClient } from "../src/book/autorest/bookApiClient";
import * as config from '../src/config';
import { RequestError } from "../src/request-error";

describe("Book - Stats API returns Internal Server Error", () => {

    const mb = new Mountebank();
    const bookApi = new BookApiClient({ baseUri: `http://localhost:5000` });

    before(async () => {
        let imposter = new Imposter().withPort(config.getStatsApiPort()).withStub(
            new Stub()
                .withResponse(new Response()
                    .withStatusCode(500)
                    .withJSONBody(new RequestError('Internal Server Error', 500)))
        );

        await mb.createImposter(imposter);
    })

    it('returns a book without stats if stat service returns internal server error', async () => {
        const book = await bookApi.books.get(1);
        expect(book.copiesSold).to.equal(null);
    })
});
