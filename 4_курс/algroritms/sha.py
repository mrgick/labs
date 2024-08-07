# SHA-256


def rotate_right(x: str, n: int):
    return x[-n:] + x[:-n]


def shift_right(x: str, n: int):
    return "0" * n + x[:-n]


def bin_xor(a, *args):
    a = [int(i) for i in a]
    for x in args:
        x = [int(i) for i in x]
        for i in range(len(a)):
            a[i] ^= x[i]
    return "".join((str(x) for x in a))


def bin_not(a):
    return "".join((str(1 if int(x) == 0 else 0) for x in a))


def bin_and(a, *args):
    a = [int(i) for i in a]
    for x in args:
        x = [int(i) for i in x]
        for i in range(len(a)):
            a[i] &= x[i]
    return "".join((str(x) for x in a))


def bin_sum(a, *args):
    a = [int(i) for i in a]
    for x in args:
        x = [int(i) for i in x]
        p = 0
        for i in range(31, -1, -1):
            p, a[i] = divmod(a[i] + x[i] + p, 2)
    z = "".join((str(x) for x in a))
    return z


def bin_to_32(x: str) -> str:
    if len(x) > 32:
        return x[-32:]
    elif len(x) < 32:
        return x.zfill(32)
    return x


def sha_256(text_original="hello world") -> str:
    ### Step 1. Data preprocessing
    # 1. convert to binary
    text = "".join(format(x, "08b") for x in bytearray(text_original, "utf-8"))
    # 2. add one to the end
    text += "1"
    # 3. add zeros while not mod 512 = 0 without last 64 bits
    text += "0" * (512 - len(text) % 512 - 64)
    assert len(text) % 512 == 448
    # 4. add length text_original in last 64 bit big-endian
    end = format(len(text_original) * 8, "08b")
    if len(end) < 64:
        end = "0" * (64 - len(end)) + end
    text += end

    ### Step 2. Initialization of hash values
    h0 = 0x6A09E667
    h1 = 0xBB67AE85
    h2 = 0x3C6EF372
    h3 = 0xA54FF53A
    h5 = 0x9B05688C
    h4 = 0x510E527F
    h6 = 0x1F83D9AB
    h7 = 0x5BE0CD19

    ### Step 3. Initialization of rounded constants
    # fmt: off
    K = [
        0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
        0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
        0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
        0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
        0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13, 0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
        0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
        0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
        0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2
    ]
    # fmt: on

    ### Step 4. Main cycle
    # every block of 512 bits, we have only one

    ### Step 5. Queue of messages
    # 1. Split to 32 bit words
    w = [text[i : i + 32] for i in range(0, 512, 32)]
    # 2. Adding 48 zeros 32 bit words
    w.extend(["0" * 32 for i in range(48)])
    # 3. Doing some magic stuff - changing words =)
    for i in range(16, 64):
        s0 = bin_xor(
            rotate_right(w[i - 15], 7),
            rotate_right(w[i - 15], 18),
            shift_right(w[i - 15], 3),
        )
        s1 = bin_xor(
            rotate_right(w[i - 2], 17),
            rotate_right(w[i - 2], 19),
            shift_right(w[i - 2], 10),
        )
        w[i] = bin_sum(w[i - 16], s0, w[i - 7], s1)

    ### Step 6. Compression
    a = bin_to_32(bin(h0)[2:])
    b = bin_to_32(bin(h1)[2:])
    c = bin_to_32(bin(h2)[2:])
    d = bin_to_32(bin(h3)[2:])
    e = bin_to_32(bin(h4)[2:])
    f = bin_to_32(bin(h5)[2:])
    g = bin_to_32(bin(h6)[2:])
    h = bin_to_32(bin(h7)[2:])

    for i in range(64):
        s1 = bin_xor(rotate_right(e, 6), rotate_right(e, 11), rotate_right(e, 25))
        ch = bin_xor(bin_and(e, f), bin_and(bin_not(e), g))
        temp1 = bin_sum(h, s1, ch, bin_to_32(bin(K[i])[2:]), w[i])
        s0 = bin_xor(rotate_right(a, 2), rotate_right(a, 13), rotate_right(a, 22))
        maj = bin_xor(bin_and(a, b), bin_and(a, c), bin_and(b, c))
        temp2 = bin_sum(s0, maj)
        h = g
        g = f
        f = e
        e = bin_sum(d, temp1)
        d = c
        c = b
        b = a
        a = bin_sum(temp1, temp2)

    ### Step 7. Modify Final Values
    h0 = bin_sum(a, bin_to_32(bin(h0)[2:]))
    h1 = bin_sum(b, bin_to_32(bin(h1)[2:]))
    h2 = bin_sum(c, bin_to_32(bin(h2)[2:]))
    h3 = bin_sum(d, bin_to_32(bin(h3)[2:]))
    h4 = bin_sum(e, bin_to_32(bin(h4)[2:]))
    h5 = bin_sum(f, bin_to_32(bin(h5)[2:]))
    h6 = bin_sum(g, bin_to_32(bin(h6)[2:]))
    h7 = bin_sum(h, bin_to_32(bin(h7)[2:]))
    ### Step 8. Concatenate Final Hash
    result = (
        str(hex(int(h0, 2))[2:])
        + str(hex(int(h1, 2))[2:])
        + str(hex(int(h2, 2))[2:])
        + str(hex(int(h3, 2))[2:])
        + str(hex(int(h4, 2))[2:])
        + str(hex(int(h5, 2))[2:])
        + str(hex(int(h6, 2))[2:])
        + str(hex(int(h7, 2))[2:])
    )
    return result

def main():
    text = "hello world"
    from hashlib import sha256
    library = sha256(text.encode('utf-8')).hexdigest()
    my = sha_256(text)
    print("Library hash   : ", library)
    print("Hash by guide  : ", my)
    assert my == library

if __name__ == "__main__":
    main()

