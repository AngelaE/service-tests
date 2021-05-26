import {assert, expect} from "chai"

import { Predicate, Response, Imposter, Mountebank, Stub, EqualPredicate, HttpMethod, DefaultStub, NotFoundResponse } from '@anev/ts-mountebank';
import { BookStatsModel } from "../src/bookstats";
import { BookApiClient } from "../src/book/autorest/bookApiClient";

const port = 12345;
const testPath = '/testpath';



describe("Book - GetBook by Id", () => {

    // only runs on local machine for now
    const mb = new Mountebank();
    const bookApi = new BookApiClient({baseUri: 'http://localhost:5000'});

    
    it('returns a book with stats', async () => {
        const bookStats: BookStatsModel = { bookId: 1, copiesSold: 2405};
 
        let imposter = new Imposter().withPort(port).withStub(
                new Stub()
                    .withPredicate(new EqualPredicate().withMethod(HttpMethod.GET).withPath(`/bookstat/1`))
                    .withResponse(new Response().withStatusCode(200).withJSONBody(bookStats))
                    )
                .withStub(new Stub().withResponse(new NotFoundResponse()));
        
        // act
        try {
            await mb.createImposter(imposter);
        }
        catch(error) {
            console.log(error);
            assert.fail('The mock response could not be created');
        }

        // act
        const bookWithStats = await bookApi.books.get(1);

        // assert
        expect(bookWithStats).to.not.be.undefined;
        expect(bookWithStats.id).to.equal(1);
        expect(bookWithStats.copiesSold).to.equal(bookStats.copiesSold);

    })
});
