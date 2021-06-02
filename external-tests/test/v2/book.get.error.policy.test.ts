import { expect } from "chai"

import { Mountebank } from '@anev/ts-mountebank';
import { BookApiClient } from "../../src/book/autorest/bookApiClient";
import * as config from '../../src/config';
import { RequestError } from "../../src/request-error";
import { StatsImposterBuilder } from "../../src/bookstats/stats-imposter-builder";

describe("v2 Book - Stats API returns Transient Error", () => {

    const mb = new Mountebank();
    const bookApi = new BookApiClient({ baseUri: `http://localhost:5000` });

    before(async () => {
        let imposter = new StatsImposterBuilder('error.policy', config.getStatsApiPort())
            .withBookStatsResponses(1, [
                new RequestError('Internal Server Error', 500),
                {bookId: 1, copiesSold: 555}
            ])
            .create();
        await mb.createImposter(imposter);
    })

    it('Book API retries getting stats on transient error and succeeds', async () => {
        const book = await bookApi.books.get(1);
        expect(book.copiesSold).to.equal(555);
    })
});
