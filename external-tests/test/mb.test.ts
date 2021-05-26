import {assert, expect} from "chai"

import { Predicate, Response, Imposter, Mountebank, Stub, EqualPredicate, HttpMethod, DefaultStub, NotFoundResponse } from '@anev/ts-mountebank';
import { BookStatsModel } from "../src/bookstats";

const port = 12345;
const testPath = '/testpath';



describe("Mountebank", () => {

    // only runs on local machine for now
    const mb = new Mountebank();

    
    it('is running', async () => {
        // act
        const isAlive = await mb.checkIsAlive(true);

        // assert
        expect(isAlive).to.be.true;
    });

    it('can create an imposter', async () => {

        const bookResponse1: BookStatsModel = { bookId: 1, copiesSold: 2405};
        const bookResponse2: BookStatsModel = { bookId: 2, copiesSold: 6324};

        let imposter = new Imposter().withPort(port).withStub(
                new Stub()
                    .withPredicate(new EqualPredicate().withMethod(HttpMethod.GET).withPath(`/bookstat/1`))
                    .withResponse(new Response().withStatusCode(200).withJSONBody(bookResponse1))
                    )
                .withStub(
                    new Stub()
                    .withPredicate(new EqualPredicate().withMethod(HttpMethod.GET).withPath(`/bookstat/2`))
                    .withResponse(new Response().withStatusCode(200).withJSONBody(bookResponse2))
                    )
                .withStub(new Stub().withResponse(new NotFoundResponse()));
        
        // act
        try {
            await mb.createImposter(imposter);
        }
        catch(error) {
            console.log(error);
            assert.fail();
        }

    })

   
});
