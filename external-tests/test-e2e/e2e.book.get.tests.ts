import { assert, expect } from "chai";
import { BookApiClient } from "../src/book/autorest/bookApiClient";
import { Stats } from "../src/bookstats";



describe("Book - GetBook by Id", () => {

    const bookApi = new BookApiClient({baseUri: `http://localhost:5000`});

    it('returns a book with stats if stats exist', async () => {
        const bookWithStats = await bookApi.books.get(1);
        expect(bookWithStats.copiesSold).to.equal(21453);
    })

    it('returns a book without stats if stats do not exist', async () => {
        const bookWithStats = await bookApi.books.get(3);
        expect(bookWithStats.copiesSold).to.equal(null);
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
